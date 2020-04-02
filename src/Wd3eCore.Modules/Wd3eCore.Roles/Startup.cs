using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.Deployment;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Recipes;
using Wd3eCore.Roles.Controllers;
using Wd3eCore.Roles.Deployment;
using Wd3eCore.Roles.Recipes;
using Wd3eCore.Roles.Services;
using Wd3eCore.Security;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Security.Services;

namespace Wd3eCore.Roles
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
            services.TryAddScoped<RoleManager<IRole>>();
            services.TryAddScoped<IRoleStore<IRole>, RoleStore>();
            services.TryAddScoped<IRoleService, RoleService>();
            services.TryAddScoped<IRoleClaimStore<IRole>, RoleStore>();
            services.AddRecipeExecutionStep<RolesStep>();

            services.AddScoped<IFeatureEventHandler, RoleUpdater>();
            services.AddScoped<IAuthorizationHandler, RolesPermissionsHandler>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            // Deployment
            services.AddTransient<IDeploymentSource, AllRolesDeploymentSource>();
            services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<AllRolesDeploymentStep>());
            services.AddScoped<IDisplayDriver<DeploymentStep>, AllRolesDeploymentStepDriver>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var adminControllerName = typeof(AdminController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "RolesIndex",
                areaName: "Wd3eCore.Roles",
                pattern: _adminOptions.AdminUrlPrefix + "/Roles/Index",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Index) }
            );
            routes.MapAreaControllerRoute(
                name: "RolesCreate",
                areaName: "Wd3eCore.Roles",
                pattern: _adminOptions.AdminUrlPrefix + "/Roles/Create",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Create) }
            );
            routes.MapAreaControllerRoute(
                name: "RolesDelete",
                areaName: "Wd3eCore.Roles",
                pattern: _adminOptions.AdminUrlPrefix + "/Roles/Delete/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Delete) }
            );
            routes.MapAreaControllerRoute(
                name: "RolesEdit",
                areaName: "Wd3eCore.Roles",
                pattern: _adminOptions.AdminUrlPrefix + "/Roles/Edit/{id}",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Edit) }
            );
        }
    }
}
