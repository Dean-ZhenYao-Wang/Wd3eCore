using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Modules;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Drivers
{
    [Feature("Wd3eCore.Users.ChangeEmail")]
    public class ChangeEmailSettingsDisplayDriver : SectionDisplayDriver<ISite, ChangeEmailSettings>
    {
        public const string GroupId = "ChangeEmailSettings";

        public override IDisplayResult Edit(ChangeEmailSettings section)
        {
            return Initialize<ChangeEmailSettings>("ChangeEmailSettings_Edit", model =>
            {
                model.AllowChangeEmail = section.AllowChangeEmail;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(ChangeEmailSettings section, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                await context.Updater.TryUpdateModelAsync(section, Prefix);
            }
            return Edit(section);
        }
    }
}
