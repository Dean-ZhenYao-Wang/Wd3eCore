using System.Threading.Tasks;

namespace Wd3eCore.Deployment
{
    /// <summary>
    /// 解析从部署计划到构建结果包的步骤。
    /// </summary>
    public interface IDeploymentSource
    {
        Task ProcessDeploymentStepAsync(DeploymentStep step, DeploymentPlanResult result);
    }
}
