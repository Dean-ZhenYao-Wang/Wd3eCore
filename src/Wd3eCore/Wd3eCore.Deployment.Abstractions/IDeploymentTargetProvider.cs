using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wd3eCore.Deployment
{
    public interface IDeploymentTargetProvider
    {
        Task<IEnumerable<DeploymentTarget>> GetDeploymentTargetsAsync();
    }
}
