using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Environment.Shell.Scope;
using Wd3eCore.Modules;
using Wd3eCore.Setup.Services;
using Wd3eCore.Workflows.Abstractions.Models;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Models;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Tenants.Workflows.Activities
{
    public class SetupTenantTask : TenantTask
    {
        private readonly IClock _clock;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;

        public SetupTenantTask(IShellSettingsManager shellSettingsManager, IShellHost shellHost, ISetupService setupService, IClock clock, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IUpdateModelAccessor updateModelAccessor, IStringLocalizer<SetupTenantTask> localizer)
            : base(shellSettingsManager, shellHost, expressionEvaluator, scriptEvaluator, localizer)
        {
            SetupService = setupService;
            _clock = clock;
            _expressionEvaluator = expressionEvaluator;
            _updateModelAccessor = updateModelAccessor;
        }

        protected ISetupService SetupService { get; }

        public override string Name => nameof(SetupTenantTask);

        public override LocalizedString Category => S["Tenant"];

        public override LocalizedString DisplayText => S["Setup Tenant Task"];

        public WorkflowExpression<string> SiteName
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> AdminUsername
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> AdminEmail
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> AdminPassword
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> DatabaseProvider
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> DatabaseConnectionString
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> DatabaseTablePrefix
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> RecipeName
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if (ShellScope.Context.Settings.Name != ShellHelper.DefaultShellName)
            {
                return Outcomes("Failed");
            }

            var tenantName = (await ExpressionEvaluator.EvaluateAsync(TenantName, workflowContext))?.Trim();

            if (string.IsNullOrWhiteSpace(tenantName))
            {
                return Outcomes("Failed");
            }

            if (!ShellHost.TryGetSettings(tenantName, out var shellSettings))
            {
                return Outcomes("Failed");
            }

            if (shellSettings.State == TenantState.Running)
            {
                return Outcomes("Failed");
            }

            if (shellSettings.State != TenantState.Uninitialized)
            {
                return Outcomes("Failed");
            }

            var siteName = (await _expressionEvaluator.EvaluateAsync(SiteName, workflowContext))?.Trim();
            var adminUsername = (await _expressionEvaluator.EvaluateAsync(AdminUsername, workflowContext))?.Trim();
            var adminEmail = (await _expressionEvaluator.EvaluateAsync(AdminEmail, workflowContext))?.Trim();
            var adminPassword = (await _expressionEvaluator.EvaluateAsync(AdminPassword, workflowContext))?.Trim();

            var databaseProvider = (await _expressionEvaluator.EvaluateAsync(DatabaseProvider, workflowContext))?.Trim();
            var databaseConnectionString = (await _expressionEvaluator.EvaluateAsync(DatabaseConnectionString, workflowContext))?.Trim();
            var databaseTablePrefix = (await _expressionEvaluator.EvaluateAsync(DatabaseTablePrefix, workflowContext))?.Trim();
            var recipeName = (await _expressionEvaluator.EvaluateAsync(RecipeName, workflowContext))?.Trim();

            if (string.IsNullOrEmpty(databaseProvider))
            {
                databaseProvider = shellSettings["DatabaseProvider"];
            }

            if (string.IsNullOrEmpty(databaseConnectionString))
            {
                databaseConnectionString = shellSettings["ConnectionString"];
            }

            if (string.IsNullOrEmpty(databaseTablePrefix))
            {
                databaseTablePrefix = shellSettings["TablePrefix"];
            }

            if (string.IsNullOrEmpty(recipeName))
            {
                recipeName = shellSettings["RecipeName"];
            }

            var recipes = await SetupService.GetSetupRecipesAsync();
            var recipe = recipes.FirstOrDefault(r => r.Name == recipeName);

            var setupContext = new SetupContext
            {
                ShellSettings = shellSettings,
                SiteName = siteName,
                EnabledFeatures = null,
                AdminUsername = adminUsername,
                AdminEmail = adminEmail,
                AdminPassword = adminPassword,
                Errors = new Dictionary<string, string>(),
                Recipe = recipe,
                SiteTimeZone = _clock.GetSystemTimeZone().TimeZoneId,
                DatabaseProvider = databaseProvider,
                DatabaseConnectionString = databaseConnectionString,
                DatabaseTablePrefix = databaseTablePrefix
            };

            var executionId = await SetupService.SetupAsync(setupContext);

            // Check if a component in the Setup failed
            if (setupContext.Errors.Any())
            {
                var updater = _updateModelAccessor.ModelUpdater;

                foreach (var error in setupContext.Errors)
                {
                    updater.ModelState.AddModelError(error.Key, error.Value);
                }

                return Outcomes("Failed");
            }

            return Outcomes("Done");
        }
    }
}
