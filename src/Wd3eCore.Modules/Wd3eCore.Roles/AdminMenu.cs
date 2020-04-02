using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Navigation;

namespace Wd3eCore.Roles
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

            builder.Add(S["Security"], security => security
                        .Add(S["Roles"], "10", installed => installed
                            .Action("Index", "Admin", "Wd3eCore.Roles")
                            .Permission(Permissions.ManageRoles)
                            .LocalNav()
                        ));

            return Task.CompletedTask;
        }
    }
}
