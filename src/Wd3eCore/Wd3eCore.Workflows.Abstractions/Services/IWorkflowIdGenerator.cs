using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IWorkflowIdGenerator
    {
        string GenerateUniqueId(Workflow workflow);
    }
}
