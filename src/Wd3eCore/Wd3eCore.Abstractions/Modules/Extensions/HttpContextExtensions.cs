using Microsoft.AspNetCore.Http;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Modules
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// 使 <see cref="HttpContext.RequestServices"/> 取到当前的 <see cref="ShellScope"/>.
        /// </summary>
        public static HttpContext UseShellScopeServices(this HttpContext httpContext)
        {
            httpContext.RequestServices = new ShellScopeServices(httpContext.RequestServices);
            return httpContext;
        }
    }
}
