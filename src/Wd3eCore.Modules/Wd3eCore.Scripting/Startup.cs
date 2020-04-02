using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Modules;
using Wd3eCore.Scripting.JavaScript;
using Wd3eCore.Scripting.Providers;

namespace Wd3eCore.Scripting
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScripting();
            services.AddJavaScriptEngine();
            services.AddSingleton<IGlobalMethodProvider, LogProvider>();
        }
    }
}
