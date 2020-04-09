using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Wd3eCore.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用对当前路径的多租户请求支持。
        /// </summary>
        public static IApplicationBuilder UseWd3eCore(this IApplicationBuilder app, Action<IApplicationBuilder> configure = null)
        {
            var env = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
            var appContext = app.ApplicationServices.GetRequiredService<IApplicationContext>();

            env.ContentRootFileProvider = new CompositeFileProvider(
                new ModuleEmbeddedFileProvider(appContext),
                env.ContentRootFileProvider);

            // 同时启动网络主机'ContentRootFileProvider'。
            app.ApplicationServices.GetRequiredService<IWebHostEnvironment>()
                .ContentRootFileProvider = env.ContentRootFileProvider;

            app.UseMiddleware<PoweredByMiddleware>();

            // 确保在出现请求并替换租户的当前服务提供者时加载了shell租户。
            app.UseMiddleware<ModularTenantContainerMiddleware>();

            configure?.Invoke(app);

            app.UseMiddleware<ModularTenantRouterMiddleware>(app.ServerFeatures);

            return app;
        }
    }
}
