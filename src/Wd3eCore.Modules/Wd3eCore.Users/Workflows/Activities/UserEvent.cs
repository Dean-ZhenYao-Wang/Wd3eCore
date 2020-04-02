using Microsoft.Extensions.Localization;
using Wd3eCore.Users.Services;
using Wd3eCore.Workflows.Activities;
using Wd3eCore.Workflows.Models;
using Wd3eCore.Workflows.Services;

namespace Wd3eCore.Users.Workflows.Activities
{
    public abstract class UserEvent : UserActivity, IEvent
    {
        public UserEvent(IUserService userService, IWorkflowScriptEvaluator scriptEvaluator, IStringLocalizer localizer) : base(userService, scriptEvaluator, localizer)
        {
        }

        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Halt();
        }

        public override ActivityExecutionResult Resume(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes("Done");
        }
    }
}
