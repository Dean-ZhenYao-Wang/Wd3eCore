using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Localization.Drivers;
using Wd3eCore.Navigation;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// Represents a localization menu in the admin site.
    /// </summary>
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer<AdminMenu> S;

        /// <summary>
        /// Creates a new instance of the <see cref="AdminMenu"/>.
        /// </summary>
        /// <param name="localizer">The <see cref="IStringLocalizer"/>.</param>
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }

        ///<inheritdocs />
        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                builder
                    .Add(S["Configuration"], NavigationConstants.AdminMenuConfigurationPosition, localization => localization
                    .AddClass("localization").Id("localization")
                        .Add(S["Settings"], settings => settings
                            .Add(S["Cultures"], S["Cultures"], entry => entry
                                .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = LocalizationSettingsDisplayDriver.GroupId })
                                .Permission(Permissions.ManageCultures)
                                .LocalNav()
                            )
                        )
                    );
            }

            return Task.CompletedTask;
        }
    }
}
