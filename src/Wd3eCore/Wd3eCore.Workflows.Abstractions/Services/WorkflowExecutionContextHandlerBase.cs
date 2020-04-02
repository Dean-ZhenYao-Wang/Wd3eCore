using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public abstract class WorkflowExecutionContextHandlerBase : IWorkflowExecutionContextHandler
    {
        public virtual Task EvaluatingExpressionAsync(WorkflowExecutionExpressionContext context)
        {
            return Task.CompletedTask;
        }

        public virtual Task EvaluatingScriptAsync(WorkflowExecutionScriptContext context)
        {
            return Task.CompletedTask;
        }

        public Task DehydrateValueAsync(SerializeWorkflowValueContext context)
        {
            return Task.CompletedTask;
        }

        public Task RehydrateValueAsync(SerializeWorkflowValueContext context)
        {
            return Task.CompletedTask;
        }
    }
}
