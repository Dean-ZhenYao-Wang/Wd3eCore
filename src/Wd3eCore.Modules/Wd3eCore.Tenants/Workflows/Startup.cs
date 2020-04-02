using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Modules;
using Wd3eCore.Tenants.Workflows.Activities;
using Wd3eCore.Tenants.Workflows.Drivers;
using Wd3eCore.Workflows.Helpers;

namespace Wd3eCore.Tenants.Workflows
{
    [RequireFeatures("Wd3eCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<DisableTenantTask, DisableTenantTaskDisplay>();
            services.AddActivity<EnableTenantTask, EnableTenantTaskDisplay>();
            services.AddActivity<CreateTenantTask, CreateTenantTaskDisplay>();
            services.AddActivity<SetupTenantTask, SetupTenantTaskDisplay>();
        }
    }
}
