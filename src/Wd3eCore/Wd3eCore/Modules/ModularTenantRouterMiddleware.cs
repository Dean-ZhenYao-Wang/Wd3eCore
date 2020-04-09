using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell.Builders;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 通过将请求转发给特定于租户的“IRouter”实例来处理请求。
    /// 它还在第一个请求时为被请求的租户初始化中间件。
    /// </summary>
    public class ModularTenantRouterMiddleware
    {
        private readonly IFeatureCollection _features;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores = new ConcurrentDictionary<string, SemaphoreSlim>();

        public ModularTenantRouterMiddleware(
            IFeatureCollection features,
            RequestDelegate next,
            ILogger<ModularTenantRouterMiddleware> logger)
        {
            _features = features;
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Begin Routing Request");
                _logger.LogInformation("开始路由请求");
            }

            var shellContext = ShellScope.Context;

            // 为当前请求定义一个PathBase，即RequestUrlPrefix。
            // 这将允许任何视图引用~/作为租户的基本url。
            // 因为IIS或其他中间件可能已经设置了它，所以我们只添加租户前缀值。
            if (!String.IsNullOrEmpty(shellContext.Settings.RequestUrlPrefix))
            {
                PathString prefix = "/" + shellContext.Settings.RequestUrlPrefix;
                httpContext.Request.PathBase += prefix;
                httpContext.Request.Path.StartsWithSegments(prefix, StringComparison.OrdinalIgnoreCase, out PathString remainingPath);
                httpContext.Request.Path = remainingPath;
            }

            // 我们需要重建管道吗?
            if (shellContext.Pipeline == null)
            {
                InitializePipeline(shellContext);
            }

            return shellContext.Pipeline.Invoke(httpContext);
        }

        private void InitializePipeline(ShellContext shellContext)
        {
            var semaphore = _semaphores.GetOrAdd(shellContext.Settings.Name, (name) => new SemaphoreSlim(1));

            // 为给定的shell构建管道不能由两个请求完成。
            semaphore.Wait();

            try
            {
                if (shellContext.Pipeline == null)
                {
                    shellContext.Pipeline = BuildTenantPipeline();
                }
            }
            finally
            {
                semaphore.Release();
                _semaphores.TryRemove(shellContext.Settings.Name, out semaphore);
            }
        }

        /// <summary>
        /// 为当前租户构建中间件管道
        /// </summary>
        /// <returns></returns>
        private IShellPipeline BuildTenantPipeline()
        {
            var appBuilder = new ApplicationBuilder(ShellScope.Context.ServiceProvider, _features);

            // 创建一个嵌套管道，以配置租户中间件管道。
            var startupFilters = appBuilder.ApplicationServices.GetService<IEnumerable<IStartupFilter>>();

            var shellPipeline = new ShellRequestPipeline();

            Action<IApplicationBuilder> configure = builder =>
            {
                ConfigureTenantPipeline(builder);
            };

            foreach (var filter in startupFilters.Reverse())
            {
                configure = filter.Configure(configure);
            }

            configure(appBuilder);

            shellPipeline.Next = appBuilder.Build();

            return shellPipeline;
        }

        private void ConfigureTenantPipeline(IApplicationBuilder appBuilder)
        {
            var startups = appBuilder.ApplicationServices.GetServices<IStartup>();

            // IStartup实例按模块依赖关系排序，默认情况下“ConfigureOrder”为0。
            // OrderBy执行稳定的排序，因此在相同的'ConfigureOrder'值中保留了顺序。
            startups = startups.OrderBy(s => s.ConfigureOrder);

            appBuilder.UseRouting().UseEndpoints(routes =>
            {
                foreach (var startup in startups)
                {
                    startup.Configure(appBuilder, routes, ShellScope.Services);
                }
            });
        }
    }
}
