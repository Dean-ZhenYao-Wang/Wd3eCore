using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Workflows.Abstractions.Models;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Models;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Tenants.Workflows.Activities
{
    public abstract class TenantActivity : Activity
    {
        protected readonly IStringLocalizer S;
     
        protected TenantActivity(IShellSettingsManager shellSettingsManager, IShellHost shellHost, IWorkflowExpressionEvaluator expressionEvaluator, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer localizer)
        {
            ShellSettingsManager = shellSettingsManager;
            ShellHost = shellHost;
            ExpressionEvaluator = expressionEvaluator;
            ScriptEvaluator = scriptEvaluator;
            S = localizer;
        }

        public WorkflowExpression<string> TenantName
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        protected IShellSettingsManager ShellSettingsManager { get; }
        protected IShellHost ShellHost { get; }
        protected IWorkflowExpressionEvaluator ExpressionEvaluator { get; }
        protected IWorkflowScriptEvaluator ScriptEvaluator { get; }

        public override LocalizedString Category => S["Tenant"];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"]);
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes("Done");
        }
    }
}
