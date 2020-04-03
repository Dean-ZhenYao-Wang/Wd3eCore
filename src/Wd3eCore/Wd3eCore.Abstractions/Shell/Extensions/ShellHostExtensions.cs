using System;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Shell
{
    public static class ShellHostExtensions
    {
        /// <summary>
        /// Retrieves the shell settings associated with the specified tenant.
        /// </summary>
        /// <returns>The shell settings associated with the tenant.</returns>
        public static ShellSettings GetSettings(this IShellHost shellHost, string tenant)
        {
            if (!shellHost.TryGetSettings(tenant, out var settings))
            {
                throw new ArgumentException("The specified tenant name is not valid.", nameof(tenant));
            }

            return settings;
        }

        /// <summary>
        /// Creates a standalone service scope that can be used to resolve local services.
        /// </summary>
        /// <param name="tenant">The tenant name related to the service scope to get.</param>
#pragma warning disable CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
        public static Task<ShellScope> GetScopeAsync(this IShellHost shellHost, string tenant)
#pragma warning restore CS1573 // 参数在 XML 注释中没有匹配的 param 标记(但其他参数有)
        {
            return shellHost.GetScopeAsync(shellHost.GetSettings(tenant));
        }
    }
}
