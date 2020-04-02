using Microsoft.Extensions.Localization;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Tenants.Workflows.Activities
{
    public abstract class TenantTask : TenantActivity, ITask
    {
        protected TenantTask(IShellSettingsManager shellSettingsManager, IShellHost shellHost, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer localizer)
            : base(shellSettingsManager, shellHost, expressionEvaluator, scriptEvaluator, localizer)
        {
        }
    }
}
