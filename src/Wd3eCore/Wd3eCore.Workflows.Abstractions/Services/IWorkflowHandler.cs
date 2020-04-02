using System.Threading.Tasks;
using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowHandler
    {
        Task CreatedAsync(WorkflowCreatedContext context);
        Task UpdatedAsync(WorkflowUpdatedContext context);
        Task DeletedAsync(WorkflowDeletedContext context);
    }
}
