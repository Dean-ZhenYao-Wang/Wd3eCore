using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Navigation;

namespace Wd3eCore.Features
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
                .Add(S["Configuration"], NavigationConstants.AdminMenuConfigurationPosition, configuration => configuration
                    .AddClass("menu-configuration").Id("configuration")
                    .Add(S["Features"], "1.2", deployment => deployment
                        .Action("Features", "Admin", new { area = "Wd3eCore.Features" })
                        .Permission(Permissions.ManageFeatures)
                        .LocalNav()
                    ), priority: 1
                );

            return Task.CompletedTask;
        }
    }
}
