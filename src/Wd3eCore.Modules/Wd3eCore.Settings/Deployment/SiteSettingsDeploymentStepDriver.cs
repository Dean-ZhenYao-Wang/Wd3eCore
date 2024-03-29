using System;
using System.Threading.Tasks;
using Wd3eCore.Deployment;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.ModelBinding;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Settings.ViewModels;

namespace Wd3eCore.Settings.Deployment
{
    public class SiteSettingsDeploymentStepDriver : DisplayDriver<DeploymentStep, SiteSettingsDeploymentStep>
    {
        public override IDisplayResult Display(SiteSettingsDeploymentStep step)
        {
            return
                Combine(
                    View("SiteSettingsDeploymentStep_Fields_Summary", step).Location("Summary", "Content"),
                    View("SiteSettingsDeploymentStep_Fields_Thumbnail", step).Location("Thumbnail", "Content")
                );
        }

        public override IDisplayResult Edit(SiteSettingsDeploymentStep step)
        {
            return Initialize<SiteSettingsDeploymentStepViewModel>("SiteSettingsDeploymentStep_Fields_Edit", model =>
            {
                model.Settings = step.Settings;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(SiteSettingsDeploymentStep step, IUpdateModel updater)
        {
            // Initializes the value to empty otherwise the model is not updated if no type is selected.
            step.Settings = Array.Empty<string>();

            await updater.TryUpdateModelAsync(step, Prefix, x => x.Settings);

            return Edit(step);
        }
    }
}
