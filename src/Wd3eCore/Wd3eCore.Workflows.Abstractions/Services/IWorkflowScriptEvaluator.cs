using System.Threading.Tasks;
using Wd3eCore.Scripting;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowScriptEvaluator
    {
        Task<T> EvaluateAsync<T>(WorkflowExpression<T> expression, WorkflowExecutionContext workflowContext, params IGlobalMethodProvider[] scopedMethodProviders);
    }
}
