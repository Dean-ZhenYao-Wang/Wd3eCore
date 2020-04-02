using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin.Controllers;
using Wd3eCore.Admin.Drivers;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Mvc.Routing;
using Wd3eCore.Navigation;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Settings;

namespace Wd3eCore.Admin
{
    public class Startup : StartupBase
    {
        private readonly AdminOptions _adminOptions;
        private readonly IShellConfiguration _configuration;

        public Startup(IOptions<AdminOptions> adminOptions, IShellConfiguration configuration)
        {
            _adminOptions = adminOptions.Value;
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddNavigation();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(AdminFilter));
                options.Filters.Add(typeof(AdminMenuFilter));

                // Ordered to be called before any global filter.
                options.Filters.Add(typeof(AdminZoneFilter), -1000);
            });

            services.AddTransient<IAreaControllerRouteMapper, AdminAreaControllerRouteMapper>();

            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IThemeSelector, AdminThemeSelector>();
            services.AddScoped<IAdminThemeService, AdminThemeService>();

            services.AddScoped<IDisplayDriver<ISite>, AdminSiteSettingsDisplayDriver>();
            services.AddScoped<IPermissionProvider, PermissionsAdminSettings>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.Configure<AdminOptions>(_configuration.GetSection("Wd3eCore.Admin"));

            services.AddSingleton<IPageRouteModelProvider, AdminPageRouteModelProvider>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "Admin",
                areaName: "Wd3eCore.Admin",
                pattern: _adminOptions.AdminUrlPrefix,
                defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
            );
        }
    }

    public class AdminPagesStartup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        public AdminPagesStartup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        public override int Order => 1000;

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RazorPagesOptions>((options) =>
            {
                options.Conventions.Add(new AdminPageRouteModelConvention(_adminOptions.AdminUrlPrefix));
            });
        }
    }
}
