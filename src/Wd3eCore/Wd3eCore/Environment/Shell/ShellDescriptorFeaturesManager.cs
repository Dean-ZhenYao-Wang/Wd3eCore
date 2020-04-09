using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Shell.Descriptor;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell
{
    public class ShellDescriptorFeaturesManager : IShellDescriptorFeaturesManager
    {
        private readonly IExtensionManager _extensionManager;
        private readonly IEnumerable<ShellFeature> _alwaysEnabledFeatures;
        private readonly IShellDescriptorManager _shellDescriptorManager;
        private readonly ILogger<ShellFeaturesManager> _logger;

        public FeatureDependencyNotificationHandler FeatureDependencyNotification { get; set; }

        public ShellDescriptorFeaturesManager(
            IExtensionManager extensionManager,
            IEnumerable<ShellFeature> shellFeatures,
            IShellDescriptorManager shellDescriptorManager,
            ILogger<ShellFeaturesManager> logger)
        {
            _extensionManager = extensionManager;
            _alwaysEnabledFeatures = shellFeatures.Where(f => f.AlwaysEnabled).ToArray();
            _shellDescriptorManager = shellDescriptorManager;
            _logger = logger;
        }

        public async Task<(IEnumerable<IFeatureInfo>, IEnumerable<IFeatureInfo>)> UpdateFeaturesAsync(ShellDescriptor shellDescriptor,
            IEnumerable<IFeatureInfo> featuresToDisable, IEnumerable<IFeatureInfo> featuresToEnable, bool force)
        {
            var alwaysEnabledIds = _alwaysEnabledFeatures.Select(sf => sf.Id).ToArray();

            var enabledFeatures = _extensionManager.GetFeatures().Where(f =>
                shellDescriptor.Features.Any(sf => sf.Id == f.Id)).ToList();

            var enabledFeatureIds = enabledFeatures.Select(f => f.Id).ToArray();

            var AllFeaturesToDisable = featuresToDisable
                .Where(f => !alwaysEnabledIds.Contains(f.Id))
                .SelectMany(feature => GetFeaturesToDisable(feature, enabledFeatureIds, force))
                .Distinct()
                .ToList();

            if (AllFeaturesToDisable.Count > 0)
            {
                foreach (var feature in AllFeaturesToDisable)
                {
                    enabledFeatures.Remove(feature);

                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Feature '{FeatureName}' was disabled", feature.Id);
                        _logger.LogInformation("特性 '{FeatureName}' 已被禁用", feature.Id);
                    }
                }
            }

            enabledFeatureIds = enabledFeatures.Select(f => f.Id).ToArray();

            var AllFeaturesToEnable = featuresToEnable
                .SelectMany(feature => GetFeaturesToEnable(feature, enabledFeatureIds, force))
                .Distinct()
                .ToList();

            if (AllFeaturesToEnable.Count > 0)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    foreach (var feature in AllFeaturesToEnable)
                    {
                        _logger.LogInformation("Enabling feature '{FeatureName}'", feature.Id);
                        _logger.LogInformation("启用特性 '{FeatureName}'", feature.Id);
                    }
                }

                enabledFeatures = enabledFeatures.Concat(AllFeaturesToEnable).Distinct().ToList();
            }

            if (AllFeaturesToDisable.Count > 0 || AllFeaturesToEnable.Count > 0)
            {
                await _shellDescriptorManager.UpdateShellDescriptorAsync(
                    shellDescriptor.SerialNumber,
                    enabledFeatures.Select(x => new ShellFeature(x.Id)).ToList(),
                    shellDescriptor.Parameters);
            }

            return (AllFeaturesToDisable, AllFeaturesToEnable);
        }

        /// <summary>
        /// 启用一个特性。
        /// </summary>
        /// <param name="featureInfo">要启用的特性信息。</param>
        /// <param name="enabledFeatureIds">目前启用的特性id列表。</param>
        /// <param name="force">布尔参数，表示该特性是否应该启用它的依赖关系。</param>
        /// <returns>枚举了要禁用的特性，如果'force'=true且依赖关系被禁用，则为空。</returns>
        private IEnumerable<IFeatureInfo> GetFeaturesToEnable(IFeatureInfo featureInfo, IEnumerable<string> enabledFeatureIds, bool force)
        {
            var featuresToEnable = _extensionManager
                .GetFeatureDependencies(featureInfo.Id)
                .Where(f => !enabledFeatureIds.Contains(f.Id))
                .ToList();

            if (featuresToEnable.Count > 1 && !force)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning(" To enable '{FeatureId}', additional features need to be enabled.", featureInfo.Id);
                    _logger.LogWarning(" 要启用 '{FeatureId}',需要启用附加特性。", featureInfo.Id);
                }
                //如果启用了{0}，那么你还需要启用{1}。
                FeatureDependencyNotification?.Invoke("If {0} is enabled, then you'll also need to enable {1}.", featureInfo, featuresToEnable.Where(f => f.Id != featureInfo.Id));

                return Enumerable.Empty<IFeatureInfo>();
            }

            return featuresToEnable;
        }

        /// <summary>
        /// 禁用一个特性。
        /// </summary>
        /// <param name="featureInfo">要禁用的特性信息。</param>
        /// <param name="enabledFeatureIds">目前启用的特性id列表。</param>
        /// <param name="force">布尔参数，表示该特性是否应该禁用其依赖的特性。</param>
        /// <returns>枚举了要启用的特性，如果'force'=true，并且启用了从属关系，则为空。</returns>
        private IEnumerable<IFeatureInfo> GetFeaturesToDisable(IFeatureInfo featureInfo, IEnumerable<string> enabledFeatureIds, bool force)
        {
            var featuresToDisable = _extensionManager
                .GetDependentFeatures(featureInfo.Id)
                .Where(f => enabledFeatureIds.Contains(f.Id))
                .ToList();

            if (featuresToDisable.Count > 1 && !force)
            {
                if (_logger.IsEnabled(LogLevel.Warning))
                {
                    _logger.LogWarning(" To disable '{FeatureId}', additional features need to be disabled.", featureInfo.Id);
                    _logger.LogWarning(" 要禁用'{FeatureId}'，需要禁用附加特性。", featureInfo.Id);
                }
                //如果{0}被禁用，那么你也需要禁用{1}。
                FeatureDependencyNotification?.Invoke("If {0} is disabled, then you'll also need to disable {1}.", featureInfo, featuresToDisable.Where(f => f.Id != featureInfo.Id));

                return Enumerable.Empty<IFeatureInfo>();
            }

            return featuresToDisable;
        }
    }
}
