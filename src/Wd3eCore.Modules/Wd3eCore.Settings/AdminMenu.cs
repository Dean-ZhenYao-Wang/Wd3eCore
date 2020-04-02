using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Navigation;

namespace Wd3eCore.Settings
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

            builder.Add(S["Configuration"], configuration => configuration
                .Add(S["Settings"], "1", settings => settings
                    .Add(S["General"], "1", entry => entry
                        .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = "general" })
                        .Permission(Permissions.ManageGroupSettings)
                        .LocalNav()
                    )
                )
            );

            return Task.CompletedTask;
        }
    }
}
