using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell.State;

namespace Wd3eCore.Environment.Shell
{
    public class NullShellStateManager : IShellStateManager
    {
        public NullShellStateManager(ILogger<NullShellStateManager> logger)
        {
            Logger = logger;
        }

        private ILogger Logger { get; set; }

        public Task<ShellState> GetShellStateAsync()
        {
            return Task.FromResult(new ShellState());
        }

        public Task UpdateEnabledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature '{FeatureName}' EnableState changed from '{FeatureState}' to '{FeatureState}'",
                             featureState.Id, featureState.EnableState, value);
                Logger.LogDebug("特性 '{FeatureName}' 启用状态从'{FeatureState}'变更为'{FeatureState}'",
                            featureState.Id, featureState.EnableState, value);
            }

            return Task.CompletedTask;
        }

        public Task UpdateInstalledStateAsync(ShellFeatureState featureState, ShellFeatureState.State value)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.LogDebug("Feature '{FeatureName}' InstallState changed from '{FeatureState}' to '{FeatureState}'", featureState.Id, featureState.InstallState, value);
                Logger.LogDebug("特性 '{FeatureName}' 安装状态由 '{FeatureState}' 更改为 '{FeatureState}'", featureState.Id, featureState.InstallState, value);
            }

            return Task.CompletedTask;
        }
    }
}
