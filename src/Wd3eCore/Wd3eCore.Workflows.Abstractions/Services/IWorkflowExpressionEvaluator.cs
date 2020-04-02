using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowExpressionEvaluator
    {
        Task<T> EvaluateAsync<T>(WorkflowExpression<T> expression, WorkflowExecutionContext workflowContext);
    }
}
