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
        /// 添加租户级缓存服务。
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

                // IMemoryCache在租户级别注册，因此每个租户都有一个实例。
                services.AddSingleton<IMemoryCache, MemoryCache>();

                // MemoryDistributedCache需要注册为单例，因为它拥有一个MemoryCache实例。
                services.AddSingleton<IDistributedCache, MemoryDistributedCache>();

                // 提供分布式缓存服务，可以返回当前作用域内的现有引用。
                services.AddScoped<IScopedDistributedCache, ScopedDistributedCache>();
            });

            return builder;
        }
    }
}
