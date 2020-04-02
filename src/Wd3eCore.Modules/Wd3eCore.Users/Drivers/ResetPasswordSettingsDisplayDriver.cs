using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Modules;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Drivers
{
    [Feature("Wd3eCore.Users.ResetPassword")]
    public class ResetPasswordSettingsDisplayDriver : SectionDisplayDriver<ISite, ResetPasswordSettings>
    {
        public const string GroupId = "ResetPasswordSettings";

        public override IDisplayResult Edit(ResetPasswordSettings section)
        {
            return Initialize<ResetPasswordSettings>("ResetPasswordSettings_Edit", model =>
            {
                model.AllowResetPassword = section.AllowResetPassword;
                model.UseSiteTheme = section.UseSiteTheme;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(ResetPasswordSettings section, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                await context.Updater.TryUpdateModelAsync(section, Prefix);
            }
            return Edit(section);
        }
    }
}
