using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.Deployment;
using Wd3eCore.DisplayManagement;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Liquid;
using Wd3eCore.Modules;
using Wd3eCore.Navigation;
using Wd3eCore.Recipes;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Settings.Deployment;
using Wd3eCore.Settings.Drivers;
using Wd3eCore.Settings.Recipes;
using Wd3eCore.Settings.Services;
using Wd3eCore.Setup.Events;

namespace Wd3eCore.Settings
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        private readonly AdminOptions _adminOptions;

        public Startup(IOptions<AdminOptions> adminOptions)
        {
            _adminOptions = adminOptions.Value;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISetupEventHandler, SetupEventHandler>();
            services.AddScoped<IPermissionProvider, Permissions>();

            services.AddRecipeExecutionStep<SettingsStep>();
            services.AddSingleton<ISiteService, SiteService>();

            // Site Settings editor
            services.AddScoped<IDisplayManager<ISite>, DisplayManager<ISite>>();
            services.AddScoped<IDisplayDriver<ISite>, DefaultSiteSettingsDisplayDriver>();
            services.AddScoped<IDisplayDriver<ISite>, ButtonsSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddScoped<ILiquidTemplateEventHandler, SiteLiquidTemplateEventHandler>();

            services.AddScoped<ITimeZoneSelector, DefaultTimeZoneSelector>();

            services.AddTransient<IDeploymentSource, SiteSettingsDeploymentSource>();
            services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<SiteSettingsDeploymentStep>());
            services.AddScoped<IDisplayDriver<DeploymentStep>, SiteSettingsDeploymentStepDriver>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            // Admin
            routes.MapAreaControllerRoute(
                name: "AdminSettings",
                areaName: "Wd3eCore.Settings",
                pattern: _adminOptions.AdminUrlPrefix + "/Settings/{groupId}",
                defaults: new { controller = "Admin", action = "Index" }
            );
        }
    }
}
