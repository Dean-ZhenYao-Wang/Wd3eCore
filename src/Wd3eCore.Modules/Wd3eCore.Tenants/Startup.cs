using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Wd3eCore.Admin;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Modules;
using Wd3eCore.Modules.FileProviders;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Setup;
using Wd3eCore.Tenants.Controllers;
using Wd3eCore.Tenants.Services;

namespace Wd3eCore.Tenants
{
    public class Startup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddSetup();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var adminControllerName = typeof(AdminController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "Tenants",
                areaName: "Wd3eCore.Tenants",
                pattern: _adminOptions.AdminUrlPrefix + "/Tenants",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Index) }
            );
            routes.MapAreaControllerRoute(
                name: "TenantsCreate",
                areaName: "Wd3eCore.Tenants",
                pattern: _adminOptions.AdminUrlPrefix + "/Tenants/Create",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Create) }
            );
            routes.MapAreaControllerRoute(
                name: "TenantsEdit",
                areaName: "Wd3eCore.Tenants",
                pattern: _adminOptions.AdminUrlPrefix + "/Tenants/Edit/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Edit) }
            );
            routes.MapAreaControllerRoute(
                name: "TenantsReload",
                areaName: "Wd3eCore.Tenants",
                pattern: _adminOptions.AdminUrlPrefix + "/Tenants/Reload/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Reload) }
            );
        }
    }

    [Feature("Wd3eCore.Tenants.FileProvider")]
    public class FileProviderStartup : StartupBase
    {
        /// <summary>
        /// The path in the tenant's App_Data folder containing the files
        /// </summary>
        private const string AssetsPath = "wwwroot";

        // Run after other middlewares
        public override int Order => 10;

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITenantFileProvider>(serviceProvider =>
            {
                var shellOptions = serviceProvider.GetRequiredService<IOptions<ShellOptions>>();
                var shellSettings = serviceProvider.GetRequiredService<ShellSettings>();

                string contentRoot = GetContentRoot(shellOptions.Value, shellSettings);

                if (!Directory.Exists(contentRoot))
                {
                    Directory.CreateDirectory(contentRoot);
                }
                return new TenantFileProvider(contentRoot);
            });

            services.AddSingleton<IStaticFileProvider>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<ITenantFileProvider>();
            });
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var tenantFileProvider = serviceProvider.GetRequiredService<ITenantFileProvider>();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = tenantFileProvider,
                DefaultContentType = "application/octet-stream",
                ServeUnknownFileTypes = true,

                // Cache the tenant static files for 7 days
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=2592000, s-max-age=31557600";
                }
            });
        }

        private string GetContentRoot(ShellOptions shellOptions, ShellSettings shellSettings)
        {
            return Path.Combine(shellOptions.ShellsApplicationDataPath, shellOptions.ShellsContainerName, shellSettings.Name, AssetsPath);
        }
    }
}
