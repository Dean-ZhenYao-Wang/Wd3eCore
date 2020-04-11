using System;
using System.Collections.Generic;
using System.Text;
using CVMDesktop.Controllers;
using CVMDesktop.dbModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Data.Migration;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using YesSql;
using YesSql.Indexes;

namespace CVMDesktop
{
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var accountControllerName = typeof(HomeController).ControllerName();

            routes.MapAreaControllerRoute(
                name: "Login",
                areaName: "Wd3eCore.Users",
                pattern: "",
                defaults: new { controller = accountControllerName, action = nameof(HomeController.Index) }
            );
            base.Configure(app, routes, serviceProvider);
        }
        public override void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IIndexProvider, SwaggerUiProvider>();
            services.AddScoped<IDataMigration, Migratons>();
        }

    }

}
