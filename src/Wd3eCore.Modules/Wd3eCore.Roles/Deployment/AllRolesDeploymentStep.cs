using Wd3eCore.Deployment;

namespace Wd3eCore.Roles.Deployment
{
    /// <summary>
    /// Adds roles to a <see cref="DeploymentPlanResult"/>.
    /// </summary>
    public class AllRolesDeploymentStep : DeploymentStep
    {
        public AllRolesDeploymentStep()
        {
            Name = "AllRoles";
        }
    }
}
