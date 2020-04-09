using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Modules;

namespace Microsoft.AspNetCore.Builder
{
    public static class PoweredByWd3eCoreExtensions
    {
        /// <summary>
        /// 配置是否使用X-Powered-By头文件。
        /// 默认值是Wd3eCore。
        /// </summary>
        /// <param name="app">模块化应用程序构建器</param>
        /// <param name="enabled">是否应在响应中包含标头</param>
        /// <returns>模块化应用程序构建器</returns>
        public static IApplicationBuilder UsePoweredByWd3eCore(this IApplicationBuilder app, bool enabled)
        {
            var options = app.ApplicationServices.GetRequiredService<IPoweredByMiddlewareOptions>();
            options.Enabled = enabled;

            return app;
        }

        /// <summary>
        /// 配置是否使用X-POWERED-BY头及其值。
        /// 默认值是Wd3eCore。
        /// </summary>
        /// <param name="app">模块化应用程序构建器</param>
        /// <param name="enabled">是否应在响应中包含标头</param>
        /// <param name="headerValue">头的值</param>
        /// <returns>模块化应用程序构建器</returns>
        public static IApplicationBuilder UsePoweredBy(this IApplicationBuilder app, bool enabled, string headerValue)
        {
            var options = app.ApplicationServices.GetRequiredService<IPoweredByMiddlewareOptions>();
            options.Enabled = enabled;
            options.HeaderValue = headerValue;

            return app;
        }
    }
}
