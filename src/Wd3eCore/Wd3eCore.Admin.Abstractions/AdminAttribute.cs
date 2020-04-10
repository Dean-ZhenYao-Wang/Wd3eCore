using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Wd3eCore.Admin
{
    /// <summary>
    /// 当应用到一个动作或控制器或页面模型时，拦截任何请求，以检查它是否适用于管理站点。
    /// 如果是这样，就会将请求标记为这样的请求，并确保用户有权访问它。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminAttribute : Attribute, IAsyncResourceFilter
    {
        public AdminAttribute()
        {
        }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            Apply(context.HttpContext);

            return next();
        }

        public static void Apply(HttpContext context)
        {
            // 这个值并不重要，它只是一个标记对象
            context.Items[typeof(AdminAttribute)] = null;
        }

        public static bool IsApplied(HttpContext context)
        {
            return context.Items.TryGetValue(typeof(AdminAttribute), out var value);
        }
    }
}
