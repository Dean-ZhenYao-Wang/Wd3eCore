using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Navigation;
using Wd3eCore.Security;

namespace Wd3eCore.Recipes
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
                .AddClass("recipes").Id("recipes")
                .Add(S["Recipes"], S["Recipes"], recipes => recipes
                    .Permission(StandardPermissions.SiteOwner)
                    .Action("Index", "Admin", new { area = "Wd3eCore.Recipes" })
                    .LocalNav())
                );

            return Task.CompletedTask;
        }
    }
}
