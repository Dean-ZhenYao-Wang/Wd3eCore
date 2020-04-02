using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.Modules;

namespace Wd3eCore.Navigation
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddNavigation();

            services.AddScoped<IShapeTableProvider, NavigationShapes>();
            services.AddScoped<IShapeTableProvider, PagerShapesTableProvider>();
            services.AddShapeAttributes<PagerShapes>();
        }
    }
}
