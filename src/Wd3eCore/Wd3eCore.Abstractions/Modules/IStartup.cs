using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 此接口的实现用于初始化模块的服务和HTTP请求管道。
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        ///获取用于命令启动程序配置服务的值。默认值为0。
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 获取用于命令启动器构建管道的值。默认值为 "Order "属性。
        /// </summary>
        int ConfigureOrder { get; }

        /// <summary>
        /// 此方法由运行时调用。使用此方法将服务添加到容器中。
        /// </summary>
        /// <param name="services">服务描述符的集合。</param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// 此方法由运行时调用。使用此方法配置HTTP请求管道。
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="routes"></param>
        /// <param name="serviceProvider"></param>
        void Configure(IApplicationBuilder builder, IEndpointRouteBuilder routes, IServiceProvider serviceProvider);
    }
}
