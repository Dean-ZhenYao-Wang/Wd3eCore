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
    public class EnableTenantTask : TenantTask
    {
        public EnableTenantTask(IShellSettingsManager shellSettingsManager, IShellHost shellHost, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<EnableTenantTask> localizer)
            : base(shellSettingsManager, shellHost, expressionEvaluator, scriptEvaluator, localizer)
        {
        }

        public override string Name => nameof(EnableTenantTask);

        public override LocalizedString Category => S["Tenant"];

        public override LocalizedString DisplayText => S["Enable Tenant Task"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Enabled"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if (ShellScope.Context.Settings.Name != ShellHelper.DefaultShellName)
            {
                return Outcomes("Failed");
            }

            var tenantName = (await ExpressionEvaluator.EvaluateAsync(TenantName, workflowContext))?.Trim();

            if (!ShellHost.TryGetSettings(tenantName, out var shellSettings))
            {
                return Outcomes("Failed");
            }

            if (shellSettings.State != TenantState.Disabled)
            {
                return Outcomes("Failed");
            }

            shellSettings.State = TenantState.Running;
            await ShellHost.UpdateShellSettingsAsync(shellSettings);

            return Outcomes("Enabled");
        }
    }
}
