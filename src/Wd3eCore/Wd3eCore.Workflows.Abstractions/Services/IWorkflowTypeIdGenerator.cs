using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowTypeIdGenerator
    {
        string GenerateUniqueId(WorkflowType workflowType);
    }
}
