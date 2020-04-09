using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Shell.State;
using Wd3eCore.Modules;

namespace Wd3eCore.Environment.Shell
{
    public interface IShellStateUpdater
    {
        Task ApplyChanges();
    }

    public class ShellStateUpdater : IShellStateUpdater
    {
        private readonly ShellSettings _settings;
        private readonly IShellStateManager _stateManager;
        private readonly IExtensionManager _extensionManager;
        private readonly IEnumerable<IFeatureEventHandler> _featureEventHandlers;

        public ShellStateUpdater(
            ShellSettings settings,
            IShellStateManager stateManager,
            IExtensionManager extensionManager,
            IEnumerable<IFeatureEventHandler> featureEventHandlers,
            ILogger<ShellStateUpdater> logger)
        {
            _settings = settings;
            _stateManager = stateManager;
            _extensionManager = extensionManager;
            _featureEventHandlers = featureEventHandlers;
            Logger = logger;
        }

        public ILogger Logger { get; set; }

        public async Task ApplyChanges()
        {
            if (Logger.IsEnabled(LogLevel.Information))
            {
                Logger.LogInformation("Applying changes for for tenant '{TenantName}'", _settings.Name);
                Logger.LogInformation("为租户'{TenantName}'应用更改", _settings.Name);
            }

            var loadedFeatures = await _extensionManager.LoadFeaturesAsync();

            var shellState = await _stateManager.GetShellStateAsync();

            // 将特性状态合并到有序列表中
            var loadedEntries = loadedFeatures
                .Select(fe => new
                {
                    Feature = fe,
                    FeatureDescriptor = fe.FeatureInfo,
                    FeatureState = shellState.Features.FirstOrDefault(s => s.Id == fe.FeatureInfo.Id),
                })
                .Where(entry => entry.FeatureState != null)
                .ToArray();

            // 查找当前模块中不可用的特性状态
            var additionalState = shellState.Features.Except(loadedEntries.Select(entry => entry.FeatureState));

            // 创建额外的存根条目，以便在丢失的特性上触发状态更改事件
            var allEntries = loadedEntries.Concat(additionalState.Select(featureState =>
            {
                var featureDescriptor = new InternalFeatureInfo(
                    featureState.Id,
                    new InternalExtensionInfo(featureState.Id)
                    );

                return new
                {
                    Feature = (FeatureEntry)new NonCompiledFeatureEntry(featureDescriptor),
                    FeatureDescriptor = (IFeatureInfo)featureDescriptor,
                    FeatureState = featureState
                };
            })).ToArray();

            // 相反的顺序禁用
            foreach (var entry in allEntries.Reverse().Where(entry => entry.FeatureState.EnableState == ShellFeatureState.State.Falling))
            {
                if (Logger.IsEnabled(LogLevel.Information))
                {
                    Logger.LogInformation("Disabling feature '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                    Logger.LogInformation("禁用特性 '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                }

                await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.DisablingAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                await _stateManager.UpdateEnabledStateAsync(entry.FeatureState, ShellFeatureState.State.Down);
                await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.DisabledAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
            }

            // 以相反的顺序卸载
            foreach (var entry in allEntries.Reverse().Where(entry => entry.FeatureState.InstallState == ShellFeatureState.State.Falling))
            {
                if (Logger.IsEnabled(LogLevel.Information))
                {
                    Logger.LogInformation("Uninstalling feature '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                    Logger.LogInformation("卸载特性'{FeatureName}'", entry.Feature.FeatureInfo.Id);
                }

                await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.UninstallingAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                await _stateManager.UpdateInstalledStateAsync(entry.FeatureState, ShellFeatureState.State.Down);
                await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.UninstalledAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
            }

            // 按顺序启动安装和启用
            foreach (var entry in allEntries.Where(entry => IsRising(entry.FeatureState)))
            {
                if (entry.FeatureState.InstallState == ShellFeatureState.State.Rising)
                {
                    if (Logger.IsEnabled(LogLevel.Information))
                    {
                        Logger.LogInformation("Installing feature '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                        Logger.LogInformation("安装特性 '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                    }

                    await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.InstallingAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                    await _stateManager.UpdateInstalledStateAsync(entry.FeatureState, ShellFeatureState.State.Up);
                    await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.InstalledAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                }
                if (entry.FeatureState.EnableState == ShellFeatureState.State.Rising)
                {
                    if (Logger.IsEnabled(LogLevel.Information))
                    {
                        Logger.LogInformation("Enabling feature '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                        Logger.LogInformation("启用特性 '{FeatureName}'", entry.Feature.FeatureInfo.Id);
                    }

                    await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.EnablingAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                    await _stateManager.UpdateEnabledStateAsync(entry.FeatureState, ShellFeatureState.State.Up);
                    await _featureEventHandlers.InvokeAsync((handler, featureInfo) => handler.EnabledAsync(featureInfo), entry.Feature.FeatureInfo, Logger);
                }
            }
        }

        private static bool IsRising(ShellFeatureState state)
        {
            return state.InstallState == ShellFeatureState.State.Rising ||
                   state.EnableState == ShellFeatureState.State.Rising;
        }
    }
}
