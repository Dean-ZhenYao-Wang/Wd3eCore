using System.Text;
using NLog;
using NLog.LayoutRenderers;
using NLog.Web.LayoutRenderers;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Logging
{
    /// <summary>
    /// Print the tenant name
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
                // If there is no ShellContext in the Features then the log is rendered from the Host.
                tenantName = HttpContextAccessor.HttpContext.Features.Get<ShellContextFeature>()?.ShellContext.Settings.Name ?? "None";
            }

            builder.Append(tenantName);
        }
    }
}
