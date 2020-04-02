using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Models;
using Wd3eCore.Environment.Shell.Scope;
using Wd3eCore.Workflows.Abstractions.Models;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Models;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Tenants.Workflows.Activities
{
    public class CreateTenantTask : TenantTask
    {
        public CreateTenantTask(IShellSettingsManager shellSettingsManager, IShellHost shellHost, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<CreateTenantTask> localizer)
            : base(shellSettingsManager, shellHost, expressionEvaluator, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(CreateTenantTask);

        public override LocalizedString Category => S["Tenant"];

        public override LocalizedString DisplayText => S["Create Tenant Task"];

        public string ContentType
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Description
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> RequestUrlPrefix
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> RequestUrlHost
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> DatabaseProvider
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> ConnectionString
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> TablePrefix
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

        public async override Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if (ShellScope.Context.Settings.Name != ShellHelper.DefaultShellName)
            {
                return Outcomes("Failed");
            }

            var tenantName = (await ExpressionEvaluator.EvaluateAsync(TenantName, workflowContext))?.Trim();

            if (string.IsNullOrEmpty(tenantName))
            {
                return Outcomes("Failed");
            }

            if (ShellHost.TryGetSettings(tenantName, out var shellSettings))
            {
                return Outcomes("Failed");
            }

            var requestUrlPrefix = (await ExpressionEvaluator.EvaluateAsync(RequestUrlPrefix, workflowContext))?.Trim();
            var requestUrlHost = (await ExpressionEvaluator.EvaluateAsync(RequestUrlHost, workflowContext))?.Trim();
            var databaseProvider = (await ExpressionEvaluator.EvaluateAsync(DatabaseProvider, workflowContext))?.Trim();
            var connectionString = (await ExpressionEvaluator.EvaluateAsync(ConnectionString, workflowContext))?.Trim();
            var tablePrefix = (await ExpressionEvaluator.EvaluateAsync(TablePrefix, workflowContext))?.Trim();
            var recipeName = (await ExpressionEvaluator.EvaluateAsync(RecipeName, workflowContext))?.Trim();

            // Creates a default shell settings based on the configuration.
            shellSettings = ShellSettingsManager.CreateDefaultSettings();

            shellSettings.Name = tenantName;

            if (!string.IsNullOrEmpty(requestUrlHost))
            {
                shellSettings.RequestUrlHost = requestUrlHost;
            }

            if (!string.IsNullOrEmpty(requestUrlPrefix))
            {
                shellSettings.RequestUrlPrefix = requestUrlPrefix;
            }

            shellSettings.State = TenantState.Uninitialized;

            if (!string.IsNullOrEmpty(connectionString))
            {
                shellSettings["ConnectionString"] = connectionString;
            }

            if (!string.IsNullOrEmpty(tablePrefix))
            {
                shellSettings["TablePrefix"] = tablePrefix;
            }

            if (!string.IsNullOrEmpty(databaseProvider))
            {
                shellSettings["DatabaseProvider"] = databaseProvider;
            }

            if (!string.IsNullOrEmpty(recipeName))
            {
                shellSettings["RecipeName"] = recipeName;
            }

            shellSettings["Secret"] = Guid.NewGuid().ToString();

            await ShellHost.UpdateShellSettingsAsync(shellSettings);

            workflowContext.LastResult = shellSettings;
            workflowContext.CorrelationId = shellSettings.Name;

            return Outcomes("Done");
        }
    }
}
