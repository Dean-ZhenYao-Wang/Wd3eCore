using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Logging
{
    /// <summary>
    /// 输出租户名称
    /// </summary>
    [LayoutRenderer(LayoutRendererName)]
    public class TenantLayoutRenderer : AspNetLayoutRendererBase
    {
        public const string LayoutRendererName = "Wd3e-tenant-name";

        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            var tenantName = ShellScope.Context?.Settings.Name;

            if (tenantName == null)
            {
                // 如果特征中没有ShellContext，那么将从Host日志中呈现。
                tenantName = HttpContextAccessor.HttpContext.Features.Get<ShellContextFeature>()?.ShellContext.Settings.Name ?? "None";
            }

            builder.Append(tenantName);
        }
    }
}
