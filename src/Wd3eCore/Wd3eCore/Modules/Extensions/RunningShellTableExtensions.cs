using System;
using Microsoft.AspNetCore.Http;
using Wd3eCore.Environment.Shell;

namespace Wd3eCore.Modules
{
    public static class RunningShellTableExtensions
    {
        public static ShellSettings Match(this IRunningShellTable table, HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var httpRequest = httpContext.Request;

            // Host属性包含从客户端设置的值。当调用UseIISIntegration()时，它将自动替换为X-Forwarded-Host的值。
            // 同样的方式，.Scheme方案包含用户设置的协议，而不是代理可能使用的协议（见X-Forwarded-Proto）。.

            return table.Match(httpRequest.Host, httpRequest.Path, true);
        }
    }
}
