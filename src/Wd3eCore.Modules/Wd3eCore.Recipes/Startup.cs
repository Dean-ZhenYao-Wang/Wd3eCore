using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.Deployment;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Recipes.Controllers;
using Wd3eCore.Recipes.RecipeSteps;
using Wd3eCore.Recipes.Services;

namespace Wd3eCore.Recipes
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
            services.AddRecipes();

            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddRecipeExecutionStep<CommandStep>();
            services.AddRecipeExecutionStep<RecipesStep>();

            services.AddDeploymentTargetHandler<RecipeDeploymentTargetHandler>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var adminControllerName = typeof(AdminController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "Recipes",
                areaName: "Wd3eCore.Recipes",
                pattern: _adminOptions.AdminUrlPrefix + "/Recipes",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Index) }
            );

            routes.MapAreaControllerRoute(
                name: "RecipesExecute",
                areaName: "Wd3eCore.Recipes",
                pattern: _adminOptions.AdminUrlPrefix + "/Recipes/Execute",
                defaults: new { controller = adminControllerName, action = nameof(AdminController.Execute) }
            );
        }
    }
}
