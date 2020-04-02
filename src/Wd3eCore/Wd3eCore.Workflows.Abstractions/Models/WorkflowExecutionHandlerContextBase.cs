namespace Wd3eCore.Workflows.Models
{
    public class WorkflowExecutionHandlerContextBase
    {
        protected WorkflowExecutionHandlerContextBase(WorkflowExecutionContext workflowContext)
        {
            WorkflowContext = workflowContext;
        }

        public WorkflowExecutionContext WorkflowContext { get; }
    }
}
