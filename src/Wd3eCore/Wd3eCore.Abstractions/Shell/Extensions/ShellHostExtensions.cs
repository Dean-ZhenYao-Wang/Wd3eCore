using System;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Scope;

namespace Wd3eCore.Environment.Shell
{
    public static class ShellHostExtensions
    {
        /// <summary>
        /// 检索与指定租户关联的shell设置。
        /// </summary>
        /// <returns>与租户关联的shell设置。</returns>
        public static ShellSettings GetSettings(this IShellHost shellHost, string tenant)
        {
            if (!shellHost.TryGetSettings(tenant, out var settings))
            {
                throw new ArgumentException("The specified tenant name is not valid./指定的租户名称无效。", nameof(tenant));
            }

            return settings;
        }

        /// <summary>
        /// 创建可用于解析本地服务的独立服务作用域。
        /// </summary>
        /// <param name="tenant">与要获取的服务作用域相关的租户名称。</param>
        public static Task<ShellScope> GetScopeAsync(this IShellHost shellHost, string tenant)
        {
            return shellHost.GetScopeAsync(shellHost.GetSettings(tenant));
        }
    }
}
