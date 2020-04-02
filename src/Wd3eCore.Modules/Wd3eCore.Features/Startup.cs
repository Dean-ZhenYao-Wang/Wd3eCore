using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.Deployment;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Features.Controllers;
using Wd3eCore.Features.Deployment;
using Wd3eCore.Features.Recipes.Executors;
using Wd3eCore.Features.Services;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Recipes;
using Wd3eCore.Security.Permissions;

namespace Wd3eCore.Features
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
            services.AddRecipeExecutionStep<FeatureStep>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddTransient<IDeploymentSource, AllFeaturesDeploymentSource>();
            services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<AllFeaturesDeploymentStep>());
            services.AddScoped<IDisplayDriver<DeploymentStep>, AllFeaturesDeploymentStepDriver>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var adminControllerName = typeof(AdminController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "Features",
                areaName: "Wd3eCore.Features",
                pattern: _adminOptions.AdminUrlPrefix + "/Features",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Features) }
            );
            routes.MapAreaControllerRoute(
                name: "FeaturesDisable",
                areaName: "Wd3eCore.Features",
                pattern: _adminOptions.AdminUrlPrefix + "/Features/Disable/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Disable) }
            );
            routes.MapAreaControllerRoute(
                name: "FeaturesEnable",
                areaName: "Wd3eCore.Features",
                pattern: _adminOptions.AdminUrlPrefix + "/Features/Enable/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Enable) }
            );
        }
    }
}
