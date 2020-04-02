using System;
using System.Web;
using Fluid;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.Data.Migration;
using Wd3eCore.DisplayManagement;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.Environment.Commands;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Liquid;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Security;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Settings;
using Wd3eCore.Setup.Events;
using Wd3eCore.Users.Commands;
using Wd3eCore.Users.Controllers;
using Wd3eCore.Users.Drivers;
using Wd3eCore.Users.Indexes;
using Wd3eCore.Users.Liquid;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.Services;
using Wd3eCore.Users.ViewModels;
using YesSql.Indexes;

namespace Wd3eCore.Users
{
    public class Startup : StartupBase
    {
        private const string LoginPath = "Login";
        private const string ChangePasswordPath = "ChangePassword";

        private readonly AdminOptions _adminOptions;
        private readonly string _tenantName;

        public Startup(IOptions<AdminOptions> adminOptions, ShellSettings shellSettings)
        {
            _adminOptions = adminOptions.Value;
            _tenantName = shellSettings.Name;
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var accountControllerName = typeof(AccountController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "Login",
                areaName: "Wd3eCore.Users",
                pattern: LoginPath,
                defaults: new { controller = accountControllerName, action = nameof(AccountController.Login) }
            );
            routes.MapAreaControllerRoute(
                name: "ChangePassword",
                areaName: "Wd3eCore.Users",
                pattern: ChangePasswordPath,
                defaults: new { controller = accountControllerName, action = nameof(AccountController.ChangePassword) }
            );

            routes.MapAreaControllerRoute(
                name: "UsersLogOff",
                areaName: "Wd3eCore.Users",
                pattern: "/Users/LogOff",
                defaults: new { controller = accountControllerName, action = nameof(AccountController.LogOff) }
            );

            var adminControllerName = typeof(AdminController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "UsersIndex",
                areaName: "Wd3eCore.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/Index",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Index) }
            );
            routes.MapAreaControllerRoute(
                name: "UsersCreate",
                areaName: "Wd3eCore.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/Create",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Create) }
            );
            routes.MapAreaControllerRoute(
                name: "UsersDelete",
                areaName: "Wd3eCore.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/Delete/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Delete) }
            );
            routes.MapAreaControllerRoute(
                name: "UsersEdit",
                areaName: "Wd3eCore.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/Edit/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Edit) }
            );
            routes.MapAreaControllerRoute(
                name: "UsersEditPassword",
                areaName: "Wd3eCore.Users",
                pattern: _adminOptions.AdminUrlPrefix + "/Users/EditPassword/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.EditPassword) }
            );

            builder.UseAuthorization();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSecurity();

            // Add ILookupNormalizer as Singleton because it is needed by UserIndexProvider
            services.TryAddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();

            // Adds the default token providers used to generate tokens for reset passwords, change email
            // and change telephone number operations, and for two factor authentication token generation.
            services.AddIdentity<IUser, IRole>().AddDefaultTokenProviders();

            // Configure the authentication options to use the application cookie scheme as the default sign-out handler.
            // This is required for security modules like the OpenID module (that uses SignOutAsync()) to work correctly.
            services.AddAuthentication(options => options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme);

            services.TryAddScoped<UserStore>();
            services.TryAddScoped<IUserStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserRoleStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserPasswordStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserEmailStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserSecurityStampStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserLoginStore<IUser>>(sp => sp.GetRequiredService<UserStore>());
            services.TryAddScoped<IUserClaimStore<IUser>>(sp => sp.GetRequiredService<UserStore>());

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "orchauth_" + HttpUtility.UrlEncode(_tenantName);

                // Don't set the cookie builder 'Path' so that it uses the 'IAuthenticationFeature' value
                // set by the pipeline and comming from the request 'PathBase' which already ends with the
                // tenant prefix but may also start by a path related e.g to a virtual folder.

                options.LoginPath = "/" + LoginPath;
                options.AccessDeniedPath = "/Error/403";

                // Disabling same-site is required for OpenID's module prompt=none support to work correctly.
                // Note: it has no practical impact on the security of the site since all endpoints are always
                // protected by antiforgery checks, that are enforced with or without this setting being changed.
                // 2019-12-10; Removed, since https://github.com/aspnet/Announcements/issues/390
                // 2020-02-17; Reenabled since we have compensation logic for backwardscompatibility
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddSingleton<IIndexProvider, UserIndexProvider>();
            services.AddSingleton<IIndexProvider, UserByRoleNameIndexProvider>();
            services.AddSingleton<IIndexProvider, UserByLoginInfoIndexProvider>();
            services.AddSingleton<IIndexProvider, UserByClaimIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<IUser>, DefaultUserClaimsPrincipalFactory>();

            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<ISetupEventHandler, SetupEventHandler>();
            services.AddScoped<ICommandHandler, UserCommands>();
            services.AddScoped<IRoleRemovedEventHandler, UserRoleRemovedEventHandler>();

            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddScoped<IDisplayDriver<ISite>, LoginSettingsDisplayDriver>();

            services.AddScoped<ILiquidTemplateEventHandler, UserLiquidTemplateEventHandler>();

            services.AddScoped<IDisplayManager<User>, DisplayManager<User>>();
            services.AddScoped<IDisplayDriver<User>, UserDisplayDriver>();
            services.AddScoped<IDisplayDriver<User>, UserButtonsDisplayDriver>();

            services.AddScoped<IThemeSelector, UsersThemeSelector>();
        }
    }

    [RequireFeatures("Wd3eCore.Liquid")]
    public class LiquidStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILiquidTemplateEventHandler, UserLiquidTemplateEventHandler>();
            services.AddLiquidFilter<HasPermissionFilter>("has_permission");
            services.AddLiquidFilter<HasClaimFilter>("has_claim");
            services.AddLiquidFilter<IsInRoleFilter>("is_in_role");
        }
    }

    [Feature("Wd3eCore.Users.ChangeEmail")]
    public class ChangeEmailStartup : StartupBase
    {
        private const string ChangeEmailPath = "ChangeEmail";
        private const string ChangeEmailConfirmationPath = "ChangeEmailConfirmation";

        static ChangeEmailStartup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ChangeEmailViewModel>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "ChangeEmail",
                areaName: "Wd3eCore.Users",
                pattern: ChangeEmailPath,
                defaults: new { controller = "ChangeEmail", action = "Index" }
            );

            routes.MapAreaControllerRoute(
                name: "ChangeEmailConfirmation",
                areaName: "Wd3eCore.Users",
                pattern: ChangeEmailConfirmationPath,
                defaults: new { controller = "ChangeEmail", action = "ChangeEmailConfirmation" }
            );
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, ChangeEmailAdminMenu>();
            services.AddScoped<IDisplayDriver<ISite>, ChangeEmailSettingsDisplayDriver>();
        }
    }

    [Feature("Wd3eCore.Users.Registration")]
    public class RegistrationStartup : StartupBase
    {
        private const string RegisterPath = "Register";

        static RegistrationStartup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<ConfirmEmailViewModel>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Register",
                areaName: "Wd3eCore.Users",
                pattern: RegisterPath,
                defaults: new { controller = "Registration", action = "Register" }
            );
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, RegistrationAdminMenu>();
            services.AddScoped<IDisplayDriver<ISite>, RegistrationSettingsDisplayDriver>();
        }
    }

    [Feature("Wd3eCore.Users.ResetPassword")]
    public class ResetPasswordStartup : StartupBase
    {
        private const string ForgotPasswordPath = "ForgotPassword";
        private const string ForgotPasswordConfirmationPath = "ForgotPasswordConfirmation";
        private const string ResetPasswordPath = "ResetPassword";
        private const string ResetPasswordConfirmationPath = "ResetPasswordConfirmation";

        static ResetPasswordStartup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<LostPasswordViewModel>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "ForgotPassword",
                areaName: "Wd3eCore.Users",
                pattern: ForgotPasswordPath,
                defaults: new { controller = "ResetPassword", action = "ForgotPassword" }
            );
            routes.MapAreaControllerRoute(
                name: "ForgotPasswordConfirmation",
                areaName: "Wd3eCore.Users",
                pattern: ForgotPasswordConfirmationPath,
                defaults: new { controller = "ResetPassword", action = "ForgotPasswordConfirmation" }
            );
            routes.MapAreaControllerRoute(
                name: "ResetPassword",
                areaName: "Wd3eCore.Users",
                pattern: ResetPasswordPath,
                defaults: new { controller = "ResetPassword", action = "ResetPassword" }
            );
            routes.MapAreaControllerRoute(
                name: "ResetPasswordConfirmation",
                areaName: "Wd3eCore.Users",
                pattern: ResetPasswordConfirmationPath,
                defaults: new { controller = "ResetPassword", action = "ResetPasswordConfirmation" }
            );
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<INavigationProvider, ResetPasswordAdminMenu>();
            services.AddScoped<IDisplayDriver<ISite>, ResetPasswordSettingsDisplayDriver>();
        }
    }
}
