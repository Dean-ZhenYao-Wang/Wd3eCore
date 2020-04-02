using Wd3eCore.Deployment;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.Roles.Deployment
{
    public class AllRolesDeploymentStepDriver : DisplayDriver<DeploymentStep, AllRolesDeploymentStep>
    {
        public override IDisplayResult Display(AllRolesDeploymentStep step)
        {
            return
                Combine(
                    View("AllRolesDeploymentStep_Summary", step).Location("Summary", "Content"),
                    View("AllRolesDeploymentStep_Thumbnail", step).Location("Thumbnail", "Content")
                );
        }

        public override IDisplayResult Edit(AllRolesDeploymentStep step)
        {
            return View("AllRolesDeploymentStep_Edit", step).Location("Content");
        }
    }
}
