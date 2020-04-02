using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Modules;

namespace Wd3eCore.HealthChecks
{
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            app.UseHealthChecks("/health/live");
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
        }
    }
}
