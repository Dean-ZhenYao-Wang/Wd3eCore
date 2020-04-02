using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Email.Drivers;
using Wd3eCore.Navigation;

namespace Wd3eCore.Email
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer<AdminMenu> S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
                return Task.CompletedTask;

            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                       .Add(S["Smtp"], S["Smtp"], entry => entry
                          .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = SmtpSettingsDisplayDriver.GroupId })
                          .Permission(Permissions.ManageEmailSettings)
                          .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
