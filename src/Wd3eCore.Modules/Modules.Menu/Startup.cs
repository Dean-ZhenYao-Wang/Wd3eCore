using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Modules.Menu.Controllers;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;

namespace Modules.Menu
{
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var homeControllerName = typeof(HomeController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "MenuManagement",
                areaName: "Modules.Menu",
                pattern: "MenuManagement",
                defaults: new { controller = homeControllerName, action = nameof(HomeController.Index) }
            );
        }
        public override void ConfigureServices(IServiceCollection services)
        {
        }

    }
}
