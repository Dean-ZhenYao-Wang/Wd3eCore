using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Models;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 该中间件将默认服务提供者替换为当前租户的服务提供者
    /// </summary>
    public class ModularTenantContainerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IShellHost _shellHost;
        private readonly IRunningShellTable _runningShellTable;

        public ModularTenantContainerMiddleware(
            RequestDelegate next,
            IShellHost shellHost,
            IRunningShellTable runningShellTable)
        {
            _next = next;
            _shellHost = shellHost;
            _runningShellTable = runningShellTable;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // 确保所有ShellContext都已加载并可用。
            await _shellHost.InitializeAsync();

            var shellSettings = _runningShellTable.Match(httpContext);

            // 只有在解决了租户的问题后，我们才会提供下一个请求。
            if (shellSettings != null)
            {
                if (shellSettings.State == TenantState.Initializing)
                {
                    httpContext.Response.Headers.Add(HeaderNames.RetryAfter, "10");
                    httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    await httpContext.Response.WriteAsync("The requested tenant is currently initializing./要求的租户目前正在初始化。");
                    return;
                }

                // 使‘RequestServices’知道当前的‘ShellScope’。
                httpContext.UseShellScopeServices();

                var shellScope = await _shellHost.GetScopeAsync(shellSettings);

                // 保存完整请求的“ShellContext”。
                httpContext.Features.Set(new ShellContextFeature
                {
                    ShellContext = shellScope.ShellContext,
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });

                await shellScope.UsingAsync(scope => _next.Invoke(httpContext));
            }
        }
    }
}
