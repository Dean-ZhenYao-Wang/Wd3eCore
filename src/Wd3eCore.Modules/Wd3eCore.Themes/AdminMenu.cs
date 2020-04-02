using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Navigation;

namespace Wd3eCore.Themes
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
                .Add(S["Design"], NavigationConstants.AdminMenuDesignPosition, design => design
                    .AddClass("themes").Id("themes")
                    .Permission(Permissions.ApplyTheme)
                    .Add(S["Themes"], "Themes", installed => installed
                        .Action("Index", "Admin", new { area = "Wd3eCore.Themes" })
                        .Permission(Permissions.ApplyTheme)
                        .LocalNav()
                    )
                );

            return Task.CompletedTask;
        }
    }
}
