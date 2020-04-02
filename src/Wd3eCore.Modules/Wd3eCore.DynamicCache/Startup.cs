using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.DisplayManagement.Descriptors;
using Wd3eCore.DisplayManagement.Implementation;
using Wd3eCore.DynamicCache.EventHandlers;
using Wd3eCore.DynamicCache.Services;
using Wd3eCore.DynamicCache.TagHelpers;
using Wd3eCore.Environment.Cache;
using Wd3eCore.Modules;

namespace Wd3eCore.DynamicCache
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // Register the type as it implements multiple interfaces
            services.AddScoped<DefaultDynamicCacheService>();
            services.AddScoped<IDynamicCacheService>(sp => sp.GetRequiredService<DefaultDynamicCacheService>());
            services.AddScoped<ITagRemovedEventHandler>(sp => sp.GetRequiredService<DefaultDynamicCacheService>());

            services.AddScoped<IShapeDisplayEvents, DynamicCacheShapeDisplayEvents>();

            services.AddShapeAttributes<CachedShapeWrapperShapes>();

            services.AddSingleton<IDynamicCache, DefaultDynamicCache>();
            services.AddSingleton<DynamicCacheTagHelperService>();
            services.AddTagHelpers<DynamicCacheTagHelper>();
        }
    }
}
