using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Modules;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// Represents a tenant event that will be registered to Wd3eShell.Activated in order to run migrations automatically.
    /// </summary>
    public class AutomaticDataMigrations : ModularTenantEvents
    {
        private readonly ShellSettings _shellSettings;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a new instance of the <see cref="AutomaticDataMigrations"/>.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/>.</param>
        /// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
        /// <param name="logger">The <see cref="ILogger"/>.</param>
        public AutomaticDataMigrations(
            IServiceProvider serviceProvider,
            ShellSettings shellSettings,
            ILogger<AutomaticDataMigrations> logger)
        {
            _serviceProvider = serviceProvider;
            _shellSettings = shellSettings;
            _logger = logger;
        }

        /// <inheritdocs />
        public override Task ActivatingAsync()
        {
            if (_shellSettings.State != Environment.Shell.Models.TenantState.Uninitialized)
            {
                _logger.LogDebug("Executing data migrations");

                var dataMigrationManager = _serviceProvider.GetService<IDataMigrationManager>();
                return dataMigrationManager.UpdateAllFeaturesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
