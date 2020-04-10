using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wd3eCore.Data
{
    /// <summary>
    /// 为<see cref="IServiceCollection"/>提供了扩展方法，以添加YesSQL <see cref="DatabaseProvider"/>s。
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <param name="name">数据库提供者的名称。</param>
        /// <param name="value">数据库提供者值，即SQL Server、MySQL。</param>
        /// <param name="hasConnectionString">数据库中是否包含连接字符串。</param>
        /// <param name="hasTablePrefix">表的前缀。</param>
        /// <param name="isDefault">数据提供者是否为默认的数据提供者</param>
        /// <param name="sampleConnectionString">连接字符串示例，例如：Server={Server Name};Database={Database Name};IntegratedSecurity=true</param>
        /// <returns></returns>
        public static IServiceCollection TryAddDataProvider(this IServiceCollection services, string name, string value, bool hasConnectionString, bool hasTablePrefix, bool isDefault, string sampleConnectionString = "")
        {
            for (var i = services.Count - 1; i >= 0; i--)
            {
                var entry = services[i];
                if (entry.ImplementationInstance != null)
                {
                    var databaseProvider = entry.ImplementationInstance as DatabaseProvider;
                    if (databaseProvider != null && String.Equals(databaseProvider.Name, name, StringComparison.OrdinalIgnoreCase))
                    {
                        services.RemoveAt(i);
                    }
                }
            }

            services.AddSingleton(new DatabaseProvider { Name = name, Value = value, HasConnectionString = hasConnectionString, HasTablePrefix = hasTablePrefix, IsDefault = isDefault, SampleConnectionString = sampleConnectionString });

            return services;
        }
    }
}
