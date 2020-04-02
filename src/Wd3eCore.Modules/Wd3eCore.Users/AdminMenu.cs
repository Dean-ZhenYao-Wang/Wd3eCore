using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wd3eCore.Modules;
using Wd3eCore.Navigation;
using Wd3eCore.Users.Drivers;

namespace Wd3eCore.Users
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

            builder.Add(S["Security"], NavigationConstants.AdminMenuSecurityPosition, security => security
                    .AddClass("security").Id("security")
                        .Add(S["Users"], "5", installed => installed
                            .Action("Index", "Admin", "Wd3eCore.Users")
                            .Permission(Permissions.ManageUsers)
                            .LocalNav()
                         )
                        .Add(S["Settings"], settings => settings
                            .Add(S["Login"], S["Login"], registration => registration
                                .Permission(Permissions.ManageUsers)
                                .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = LoginSettingsDisplayDriver.GroupId })
                                .LocalNav()
                                )
                            )
                       );

            return Task.CompletedTask;
        }
    }

    [Feature("Wd3eCore.Users.ChangeEmail")]
    public class ChangeEmailAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public ChangeEmailAdminMenu(IStringLocalizer<ChangeEmailAdminMenu> localizer)
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
                .Add(S["Security"], security => security
                    .Add(S["Settings"], settings => settings
                        .Add(S["Email"], S["Email"], registration => registration
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = ChangeEmailSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }

    [Feature("Wd3eCore.Users.Registration")]
    public class RegistrationAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public RegistrationAdminMenu(IStringLocalizer<RegistrationAdminMenu> localizer)
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
                .Add(S["Security"], security => security
                    .Add(S["Settings"], settings => settings
                        .Add(S["Registration"], S["Registration"], registration => registration
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = RegistrationSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }

    [Feature("Wd3eCore.Users.ResetPassword")]
    public class ResetPasswordAdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;

        public ResetPasswordAdminMenu(IStringLocalizer<ResetPasswordAdminMenu> localizer)
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
                .Add(S["Security"], security => security
                    .Add(S["Settings"], settings => settings
                        .Add(S["Reset password"], S["Reset password"], password => password
                            .Permission(Permissions.ManageUsers)
                            .Action("Index", "Admin", new { area = "Wd3eCore.Settings", groupId = ResetPasswordSettingsDisplayDriver.GroupId })
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }
}
