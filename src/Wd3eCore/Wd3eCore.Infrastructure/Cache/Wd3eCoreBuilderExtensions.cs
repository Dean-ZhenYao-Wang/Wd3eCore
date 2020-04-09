using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Wd3eCore.Environment.Cache;
using Wd3eCore.Environment.Cache.CacheContextProviders;
using Wd3eCore.Infrastructure.Cache;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds tenant level caching services.
        /// </summary>
        public static Wd3eCoreBuilder AddCaching(this Wd3eCoreBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<ITagCache, DefaultTagCache>();
                services.AddSingleton<ISignal, Signal>();
                services.AddScoped<ICacheContextManager, CacheContextManager>();
                services.AddScoped<ICacheScopeManager, CacheScopeManager>();

                services.AddScoped<ICacheContextProvider, FeaturesCacheContextProvider>();
                services.AddScoped<ICacheContextProvider, QueryCacheContextProvider>();
                services.AddScoped<ICacheContextProvider, RolesCacheContextProvider>();
                services.AddScoped<ICacheContextProvider, RouteCacheContextProvider>();
                services.AddScoped<ICacheContextProvider, UserCacheContextProvider>();
                services.AddScoped<ICacheContextProvider, KnownValueCacheContextProvider>();

                // IMemoryCache is registered at the tenant level so that there is one instance for each tenant.
                services.AddSingleton<IMemoryCache, MemoryCache>();

                // MemoryDistributedCache needs to be registered as a singleton as it owns a MemoryCache instance.
                services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

                // Provides a distributed cache service that can return existing references in the current scope.
                services.AddScoped<IScopedDistributedCache, ScopedDistributedCache>();
            });

            return builder;
        }
    }
}
