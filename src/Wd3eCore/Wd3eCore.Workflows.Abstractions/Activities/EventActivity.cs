using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Activities
{
    public abstract class EventActivity : Activity, IEvent
    {
        public override ActivityExecutionResult Execute(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            // Halt the workflow to wait for the event to occur.
            return Halt();
        }
    }
}
