using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Modules;

namespace Wd3eCore.Data.Migration
{
    /// <summary>
    /// 代表将被注册到Wd3eShell.Activated的租户事件，以便自动运行迁移。
    /// </summary>
    public class AutomaticDataMigrations : ModularTenantEvents
    {
        private readonly ShellSettings _shellSettings;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 创建一个新的<see cref="AutomaticDataMigrations"/>实例。
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/></param>
        /// <param name="shellSettings"><see cref="ShellSettings"/></param>
        /// <param name="logger"><see cref="ILogger"/></param>
        public AutomaticDataMigrations(
            IServiceProvider serviceProvider,
            ShellSettings shellSettings,
            ILogger<AutomaticDataMigrations> logger)
        {
            _serviceProvider = serviceProvider;
            _shellSettings = shellSettings;
            _logger = logger;
        }

        /// <inheritdocs />
        public override Task ActivatingAsync()
        {
            if (_shellSettings.State != Environment.Shell.Models.TenantState.Uninitialized)
            {
                _logger.LogDebug("Executing data migrations");
                _logger.LogDebug("执行数据迁移");

                var dataMigrationManager = _serviceProvider.GetService<IDataMigrationManager>();
                return dataMigrationManager.UpdateAllFeaturesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
