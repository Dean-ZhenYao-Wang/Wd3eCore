using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Admin.Drivers;
using Wd3eCore.Navigation;

namespace Wd3eCore.Admin
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Configuration"], design => design
                    .Add(S["Settings"], settings => settings
                        .Add(S["Admin"], S["Admin"], zones => zones
                            .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = AdminSiteSettingsDisplayDriver.GroupId })
                            .Permission(PermissionsAdminSettings.ManageAdminSettings)
                            .LocalNav()
                        )
                    ));

            return Task.CompletedTask;
        }
    }
}
