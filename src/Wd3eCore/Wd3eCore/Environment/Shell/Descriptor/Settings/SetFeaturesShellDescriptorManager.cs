using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Descriptor.Settings
{
    /// <summary>
    /// 通过返回一个具有指定特性集的单一租户来实现 <see cref="IShellDescriptorManager"/>。
    /// 这个类可以作为一个单例类注册，因为它的状态永远不会改变。
    /// </summary>
    public class SetFeaturesShellDescriptorManager : IShellDescriptorManager
    {
        private readonly IEnumerable<ShellFeature> _shellFeatures;
        private ShellDescriptor _shellDescriptor;

        public SetFeaturesShellDescriptorManager(IEnumerable<ShellFeature> shellFeatures)
        {
            _shellFeatures = shellFeatures;
        }

        public Task<ShellDescriptor> GetShellDescriptorAsync()
        {
            if (_shellDescriptor == null)
            {
                _shellDescriptor = new ShellDescriptor
                {
                    Features = _shellFeatures.Distinct().ToList()
                };
            }

            return Task.FromResult(_shellDescriptor);
        }

        public Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters)
        {
            return Task.CompletedTask;
        }
    }
}
