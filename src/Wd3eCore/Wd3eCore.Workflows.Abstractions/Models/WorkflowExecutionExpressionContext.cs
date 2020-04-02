using Fluid;

namespace Wd3eCore.Workflows.Models
{
    public class WorkflowExecutionExpressionContext : WorkflowExecutionHandlerContextBase
    {
        public WorkflowExecutionExpressionContext(TemplateContext templateContext, WorkflowExecutionContext workflowExecutionContext) : base(workflowExecutionContext)
        {
            TemplateContext = templateContext;
        }

        public TemplateContext TemplateContext { get; }
    }
}
