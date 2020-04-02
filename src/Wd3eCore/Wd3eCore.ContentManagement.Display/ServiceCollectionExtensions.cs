using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.ContentManagement.Display.ContentDisplay;
using Wd3eCore.ContentManagement.Display.Liquid;
using Wd3eCore.ContentManagement.Display.Placement;
using Wd3eCore.DisplayManagement.Descriptors.ShapePlacementStrategy;
using Wd3eCore.Liquid;

namespace Wd3eCore.ContentManagement.Display
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddContentManagementDisplay(this IServiceCollection services)
        {
            services.TryAddTransient<IContentItemDisplayManager, ContentItemDisplayManager>();
            services.TryAddEnumerable(new ServiceDescriptor(typeof(IContentDisplayHandler), typeof(ContentItemDisplayCoordinator), ServiceLifetime.Scoped));

            services.AddScoped<IPlacementNodeFilterProvider, ContentTypePlacementNodeFilterProvider>();
            services.AddScoped<IPlacementNodeFilterProvider, ContentPartPlacementNodeFilterProvider>();

            services.AddScoped<IContentPartDisplayDriverResolver, ContentPartDisplayDriverResolver>();
            services.AddScoped<IContentFieldDisplayDriverResolver, ContentFieldDisplayDriverResolver>();

            services.AddOptions<ContentDisplayOptions>();

            services.AddLiquidFilter<ConsoleLogFilter>("console_log");

            return services;
        }
    }
}
