using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Admin;
using Wd3eCore.DisplayManagement.Handlers;
using Wd3eCore.Email.Controllers;
using Wd3eCore.Email.Drivers;
using Wd3eCore.Email.Services;
using Wd3eCore.Modules;
using Wd3eCore.Mvc.Core.Utilities;
using Wd3eCore.Navigation;
using Wd3eCore.Security.Permissions;
using Wd3eCore.Settings;

namespace Wd3eCore.Email
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
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, SmtpSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();

            services.AddTransient<IConfigureOptions<SmtpSettings>, SmtpSettingsConfiguration>();
            services.AddScoped<ISmtpService, SmtpService>();
        }

        public override void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "EmailIndex",
                areaName: "Wd3eCore.Email",
                pattern: _adminOptions.AdminUrlPrefix + "/Email/Index",
                defaults: new { controller = typeof(AdminController).ControllerName(), action = nameof(AdminController.Index) }
            );
        }
    }
}
