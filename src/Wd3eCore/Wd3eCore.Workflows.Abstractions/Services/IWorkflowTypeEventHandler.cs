using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowTypeEventHandler
    {
        Task CreatedAsync(WorkflowTypeCreatedContext context);
        Task UpdatedAsync(WorkflowTypeUpdatedContext context);
        Task DeletedAsync(WorkflowTypeDeletedContext context);
    }
}
