using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Modules;
using Wd3eCore.Users.Models;
using Wd3eCore.Users.TimeZone.Drivers;
using Wd3eCore.Users.TimeZone.Services;

namespace Wd3eCore.Users.TimeZone
{
    [Feature("Wd3eCore.Users.TimeZone")]
    public class Startup : StartupBase
    {
        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITimeZoneSelector, UserTimeZoneSelector>();
            services.AddScoped<UserTimeZoneService>();

            services.AddScoped<IDisplayDriver<User>, UserTimeZoneDisplayDriver>();
        }
    }
}
