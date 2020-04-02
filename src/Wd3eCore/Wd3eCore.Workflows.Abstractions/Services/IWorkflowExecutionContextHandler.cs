using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowExecutionContextHandler
    {
        Task EvaluatingExpressionAsync(WorkflowExecutionExpressionContext context);
        Task EvaluatingScriptAsync(WorkflowExecutionScriptContext context);
        Task DehydrateValueAsync(SerializeWorkflowValueContext context);
        Task RehydrateValueAsync(SerializeWorkflowValueContext context);
    }
}
