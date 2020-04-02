using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Wd3eCore.Modules;
using Wd3eCore.ResourceManagement;

namespace Wd3eCore.Resources
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IResourceManifestProvider, ResourceManifest>();
            serviceCollection.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
        }
    }
}
