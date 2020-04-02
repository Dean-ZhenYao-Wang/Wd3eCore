using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Entities;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Views;
using Wd3eCore.Modules;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Drivers
{
    [Feature("Wd3eCore.Users.Registration")]
    public class RegistrationSettingsDisplayDriver : SectionDisplayDriver<ISite, RegistrationSettings>
    {
        public const string GroupId = "RegistrationSettings";

        public override IDisplayResult Edit(RegistrationSettings section)
        {
            return Initialize<RegistrationSettings>("RegistrationSettings_Edit", model =>
            {
                model.UsersCanRegister = section.UsersCanRegister;
                model.UsersMustValidateEmail = section.UsersMustValidateEmail;
                model.UseSiteTheme = section.UseSiteTheme;
                model.NoPasswordForExternalUsers = section.NoPasswordForExternalUsers;
                model.NoUsernameForExternalUsers = section.NoUsernameForExternalUsers;
                model.NoEmailForExternalUsers = section.NoEmailForExternalUsers;
                model.UseScriptToGenerateUsername = section.UseScriptToGenerateUsername;
                model.GenerateUsernameScript = section.GenerateUsernameScript;
            }).Location("Content:5").OnGroup(GroupId);
        }

        public override async Task<IDisplayResult> UpdateAsync(RegistrationSettings section, BuildEditorContext context)
        {
            if (context.GroupId == GroupId)
            {
                await context.Updater.TryUpdateModelAsync(section, Prefix);
            }
            return Edit(section);
        }
    }
}
