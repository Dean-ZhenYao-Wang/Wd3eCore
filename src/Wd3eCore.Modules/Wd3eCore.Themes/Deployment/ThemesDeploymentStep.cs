using Wd3eCore.Deployment;

namespace Wd3eCore.Themes.Deployment
{
    /// <summary>
    /// Adds the currently selected admin theme and site theme to a <see cref="DeploymentPlanResult"/>.
    /// </summary>
    public class ThemesDeploymentStep : DeploymentStep
    {
        public ThemesDeploymentStep()
        {
            Name = "Themes";
        }
    }
}
