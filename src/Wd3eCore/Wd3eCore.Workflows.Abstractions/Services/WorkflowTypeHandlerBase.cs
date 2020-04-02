using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public abstract class WorkflowTypeHandlerBase : IWorkflowTypeEventHandler
    {
        public virtual Task CreatedAsync(WorkflowTypeCreatedContext context)
        {
            return Task.CompletedTask;
        }

        public virtual Task DeletedAsync(WorkflowTypeDeletedContext context)
        {
            return Task.CompletedTask;
        }

        public virtual Task UpdatedAsync(WorkflowTypeUpdatedContext context)
        {
            return Task.CompletedTask;
        }
    }
}
