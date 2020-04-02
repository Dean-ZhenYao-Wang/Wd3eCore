using Wd3eCore.Workflows.Models;

namespace Wd3eCore.Workflows.Services
{
    public interface IActivityIdGenerator
    {
        string GenerateUniqueId(ActivityRecord activityRecord);
    }
}
