using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Descriptor;
using Wd3eCore.Environment.Shell.Descriptor.Models;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Modules;
using Wd3eCore.Recipes.Models;
using Wd3eCore.Recipes.Services;
using Wd3eCore.Setup.Events;
using YesSql;

namespace Wd3eCore.Setup.Services
{
    /// <summary>
    /// Represents a setup service.
    /// </summary>
    public class SetupService : ISetupService
    {
        private readonly IShellHost _shellHost;
        private readonly IShellContextFactory _shellContextFactory;
        private readonly IEnumerable<IRecipeHarvester> _recipeHarvesters;
        private readonly ILogger _logger;
        private readonly IStringLocalizer<SetupService> S;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly string _applicationName;
        private IEnumerable<RecipeDescriptor> _recipes;

        /// <summary>
        /// Creates a new instance of <see cref="SetupService"/>.
        /// </summary>
        /// <param name="shellHost">The <see cref="IShellHost"/>.</param>
        /// <param name="hostingEnvironment">The <see cref="IHostEnvironment"/>.</param>
        /// <param name="shellContextFactory">The <see cref="IShellContextFactory"/>.</param>
        /// <param name="runningShellTable">The <see cref="IRunningShellTable"/>.</param>
        /// <param name="recipeHarvesters">A list of <see cref="IRecipeHarvester"/>s.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        /// <param name="stringLocalizer">The <see cref="IStringLocalizer"/>.</param>
        /// <param name="applicationLifetime">The <see cref="IHostApplicationLifetime"/>.</param>
        public SetupService(
            IShellHost shellHost,
            IHostEnvironment hostingEnvironment,
            IShellContextFactory shellContextFactory,
            IRunningShellTable runningShellTable,
            IEnumerable<IRecipeHarvester> recipeHarvesters,
            ILogger<SetupService> logger,
            IStringLocalizer<SetupService> stringLocalizer,
            IHostApplicationLifetime applicationLifetime
            )
        {
            _shellHost = shellHost;
            _applicationName = hostingEnvironment.ApplicationName;
            _shellContextFactory = shellContextFactory;
            _recipeHarvesters = recipeHarvesters;
            _logger = logger;
            S = stringLocalizer;
            _applicationLifetime = applicationLifetime;
        }

        /// <inheridoc />
        public async Task<IEnumerable<RecipeDescriptor>> GetSetupRecipesAsync()
        {
            if (_recipes == null)
            {
                var recipeCollections = await Task.WhenAll(_recipeHarvesters.Select(x => x.HarvestRecipesAsync()));
                _recipes = recipeCollections.SelectMany(x => x).Where(x => x.IsSetupRecipe).ToArray();
            }

            return _recipes;
        }

        /// <inheridoc />
        public async Task<string> SetupAsync(SetupContext context)
        {
            var initialState = context.ShellSettings.State;
            try
            {
                var executionId = await SetupInternalAsync(context);

                if (context.Errors.Any())
                {
                    context.ShellSettings.State = initialState;
                }

                return executionId;
            }
            catch
            {
                context.ShellSettings.State = initialState;
                throw;
            }
        }

        private async Task<string> SetupInternalAsync(SetupContext context)
        {
            string executionId;

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Running setup for tenant '{TenantName}'.", context.ShellSettings.Name);
            }

            // Features to enable for Setup
            string[] hardcoded =
            {
                _applicationName,
                "Wd3eCore.Features",
                "Wd3eCore.Scripting",
                "Wd3eCore.Recipes"
            };

            context.EnabledFeatures = hardcoded.Union(context.EnabledFeatures ?? Enumerable.Empty<string>()).Distinct().ToList();

            // Set shell state to "Initializing" so that subsequent HTTP requests are responded to with "Service Unavailable" while Wd3e is setting up.
            context.ShellSettings.State = TenantState.Initializing;

            var shellSettings = new ShellSettings(context.ShellSettings);

            if (string.IsNullOrEmpty(shellSettings["DatabaseProvider"]))
            {
                shellSettings["DatabaseProvider"] = context.DatabaseProvider;
                shellSettings["ConnectionString"] = context.DatabaseConnectionString;
                shellSettings["TablePrefix"] = context.DatabaseTablePrefix;
            }

            if (String.IsNullOrWhiteSpace(shellSettings["DatabaseProvider"]))
            {
                throw new ArgumentException($"{nameof(context.DatabaseProvider)} is required");
            }

            // Creating a standalone environment based on a "minimum shell descriptor".
            // In theory this environment can be used to resolve any normal components by interface, and those
            // components will exist entirely in isolation - no crossover between the safemode container currently in effect
            // It is used to initialize the database before the recipe is run.

            var shellDescriptor = new ShellDescriptor
            {
                Features = context.EnabledFeatures.Select(id => new ShellFeature { Id = id }).ToList()
            };

            using (var shellContext = await _shellContextFactory.CreateDescribedContextAsync(shellSettings, shellDescriptor))
            {
                await shellContext.CreateScope().UsingAsync(async scope =>
                {
                    IStore store;

                    try
                    {
                        store = scope.ServiceProvider.GetRequiredService<IStore>();
                    }
                    catch (Exception e)
                    {
                        // Tables already exist or database was not found

                        // The issue is that the user creation needs the tables to be present,
                        // if the user information is not valid, the next POST will try to recreate the
                        // tables. The tables should be rolled back if one of the steps is invalid,
                        // unless the recipe is executing?

                        _logger.LogError(e, "An error occurred while initializing the datastore.");
                        context.Errors.Add("DatabaseProvider", S["An error occurred while initializing the datastore: {0}", e.Message]);
                        return;
                    }

                    // Create the "minimum shell descriptor"
                    await scope
                        .ServiceProvider
                        .GetService<IShellDescriptorManager>()
                        .UpdateShellDescriptorAsync(0,
                            shellContext.Blueprint.Descriptor.Features,
                            shellContext.Blueprint.Descriptor.Parameters);
                });

                if (context.Errors.Any())
                {
                    return null;
                }

                executionId = Guid.NewGuid().ToString("n");

                var recipeExecutor = shellContext.ServiceProvider.GetRequiredService<IRecipeExecutor>();

                await recipeExecutor.ExecuteAsync(executionId, context.Recipe, new
                {
                    context.SiteName,
                    context.AdminUsername,
                    context.AdminEmail,
                    context.AdminPassword,
                    context.DatabaseProvider,
                    context.DatabaseConnectionString,
                    context.DatabaseTablePrefix
                },
                _applicationLifetime.ApplicationStopping);
            }

            // Reloading the shell context as the recipe has probably updated its features
            using (var shellContext = await _shellHost.CreateShellContextAsync(shellSettings))
            {
                await shellContext.CreateScope().UsingAsync(async scope =>
                {
                    void reportError(string key, string message)
                    {
                        context.Errors[key] = message;
                    }

                    // Invoke modules to react to the setup event
                    var setupEventHandlers = scope.ServiceProvider.GetServices<ISetupEventHandler>();
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<SetupService>>();

                    await setupEventHandlers.InvokeAsync((handler, context) => handler.Setup(
                        context.SiteName,
                        context.AdminUsername,
                        context.AdminEmail,
                        context.AdminPassword,
                        context.DatabaseProvider,
                        context.DatabaseConnectionString,
                        context.DatabaseTablePrefix,
                        context.SiteTimeZone,
                        reportError
                    ), context, logger);
                });

                if (context.Errors.Any())
                {
                    return executionId;
                }
            }

            // Update the shell state
            shellSettings.State = TenantState.Running;
            await _shellHost.UpdateShellSettingsAsync(shellSettings);

            return executionId;
        }
    }
}
