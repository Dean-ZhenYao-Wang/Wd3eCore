using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.ResourceManagement;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for managing resources.
        /// </summary>
        public static IServiceCollection AddResourceManagement(this IServiceCollection services)
        {
            services.TryAddScoped<IResourceManager, ResourceManager>();
            services.TryAddSingleton<IResourceManifestState, ResourceManifestState>();

            return services;
        }
    }
}
