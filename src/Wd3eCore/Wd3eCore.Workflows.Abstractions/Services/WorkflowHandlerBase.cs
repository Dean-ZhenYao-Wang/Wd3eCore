using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public abstract class WorkflowHandlerBase : IWorkflowHandler
    {
        public virtual Task CreatedAsync(WorkflowCreatedContext context)
        {
            return Task.CompletedTask;
        }

        public virtual Task UpdatedAsync(WorkflowUpdatedContext context)
        {
            return Task.CompletedTask;
        }

        public virtual Task DeletedAsync(WorkflowDeletedContext context)
        {
            return Task.CompletedTask;
        }
    }
}
