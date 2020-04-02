using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.Entities.Scripting;
using Wd3eCore.Scripting;

namespace Wd3eCore.Entities
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdGeneration(this IServiceCollection services)
        {
            services.TryAddSingleton<IIdGenerator, DefaultIdGenerator>();
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IGlobalMethodProvider, IdGeneratorMethod>());
            return services;
        }
    }
}
