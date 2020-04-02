using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace Wd3eCore.Deployment.Services
{
    public interface IDeploymentManager
    {
        Task ExecuteDeploymentPlanAsync(DeploymentPlan deploymentPlan, DeploymentPlanResult result);
        Task<IEnumerable<DeploymentTarget>> GetDeploymentTargetsAsync();
        Task ImportDeploymentPackageAsync(IFileProvider deploymentPackage);
    }
}
