using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell
{
    public class ShellFeaturesManager : IShellFeaturesManager
    {
        private readonly IExtensionManager _extensionManager;
        private readonly ShellDescriptor _shellDescriptor;
        private readonly IShellDescriptorFeaturesManager _shellDescriptorFeaturesManager;

        public ShellFeaturesManager(
            IExtensionManager extensionManager,
            ShellDescriptor shellDescriptor,
            IShellDescriptorFeaturesManager shellDescriptorFeaturesManager)
        {
            _extensionManager = extensionManager;
            _shellDescriptor = shellDescriptor;
            _shellDescriptorFeaturesManager = shellDescriptorFeaturesManager;
        }

        public Task<IEnumerable<IFeatureInfo>> GetEnabledFeaturesAsync()
        {
            return Task.FromResult(_extensionManager.GetFeatures().Where(f => _shellDescriptor.Features.Any(sf => sf.Id == f.Id)));
        }

        public Task<IEnumerable<IFeatureInfo>> GetAlwaysEnabledFeaturesAsync()
        {
            return Task.FromResult(_extensionManager.GetFeatures().Where(f => f.IsAlwaysEnabled || _shellDescriptor.Features.Any(sf => sf.Id == f.Id && sf.AlwaysEnabled)));
        }

        public Task<IEnumerable<IFeatureInfo>> GetDisabledFeaturesAsync()
        {
            return Task.FromResult(_extensionManager.GetFeatures().Where(f => _shellDescriptor.Features.All(sf => sf.Id != f.Id)));
        }

        public Task<(IEnumerable<IFeatureInfo>, IEnumerable<IFeatureInfo>)> UpdateFeaturesAsync(IEnumerable<IFeatureInfo> featuresToDisable, IEnumerable<IFeatureInfo> featuresToEnable, bool force)
        {
            return _shellDescriptorFeaturesManager.UpdateFeaturesAsync(_shellDescriptor, featuresToDisable, featuresToEnable, force);
        }

        public Task<IEnumerable<IExtensionInfo>> GetEnabledExtensionsAsync()
        {
            // 启用的扩展是那些至少有一个启用的特性的扩展。
            var enabledIds = _extensionManager.GetFeatures().Where(f => _shellDescriptor
                .Features.Any(sf => sf.Id == f.Id)).Select(f => f.Extension.Id).Distinct().ToArray();

            // 扩展仍然按照它们最初的特性的权重排序。
            return Task.FromResult(_extensionManager.GetExtensions().Where(e => enabledIds.Contains(e.Id)));
        }
    }
}
