using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Email.Workflows.Activities;
using Wd3eCore.Email.Workflows.Drivers;
using Wd3eCore.Modules;
using Wd3eCore.Workflows.Helpers;

namespace Wd3eCore.Email.Workflows
{
    [RequireFeatures("Wd3eCore.Workflows")]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddActivity<EmailTask, EmailTaskDisplay>();
        }
    }
}
