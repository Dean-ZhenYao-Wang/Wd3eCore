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
    public class DisableTenantTask : TenantTask
    {
        public DisableTenantTask(IShellSettingsManager shellSettingsManager, IShellHost shellHost, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer<DisableTenantTask> localizer)
            : base(shellSettingsManager, shellHost, expressionEvaluator, scriptEvaluator, localizer)
        {
        }
        public override string Name => nameof(DisableTenantTask);

        public override LocalizedString Category => S["Tenant"];

        public override LocalizedString DisplayText => S["Disable Tenant Task"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Disabled"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            if (ShellScope.Context.Settings.Name != ShellHelper.DefaultShellName)
            {
                return Outcomes("Failed");
            }

            var tenantName = (await ExpressionEvaluator.EvaluateAsync(TenantName, workflowContext))?.Trim();

            if (tenantName == ShellHelper.DefaultShellName)
            {
                return Outcomes("Failed");
            }

            if (!ShellHost.TryGetSettings(tenantName?.Trim(), out var shellSettings))
            {
                return Outcomes("Failed");
            }

            if (shellSettings.State != TenantState.Running)
            {
                return Outcomes("Failed");
            }

            shellSettings.State = TenantState.Disabled;
            await ShellHost.UpdateShellSettingsAsync(shellSettings);

            return Outcomes("Disabled");
        }
    }
}
