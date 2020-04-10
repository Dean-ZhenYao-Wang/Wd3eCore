using System.Collections.Generic;

namespace Wd3eCore.Deployment
{
    /// <summary>
    /// 由源代码构建的部署计划的状态。
    /// </summary>
    public class DeploymentPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DeploymentStep> DeploymentSteps { get; } = new List<DeploymentStep>();
    }
}
