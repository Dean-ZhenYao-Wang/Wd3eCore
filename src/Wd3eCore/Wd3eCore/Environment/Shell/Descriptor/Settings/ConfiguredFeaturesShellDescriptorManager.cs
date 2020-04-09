using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Descriptor.Settings
{
    /// <summary>
    /// 通过返回配置中的特性来实现 <see cref="IShellDescriptorManager"/>。
    /// </summary>
    public class ConfiguredFeaturesShellDescriptorManager : IShellDescriptorManager
    {
        private readonly IShellConfiguration _shellConfiguration;
        private readonly IEnumerable<ShellFeature> _alwaysEnabledFeatures;
        private ShellDescriptor _shellDescriptor;

        public ConfiguredFeaturesShellDescriptorManager(
            IShellConfiguration shellConfiguration,
            IEnumerable<ShellFeature> shellFeatures)
        {
            _shellConfiguration = shellConfiguration;
            _alwaysEnabledFeatures = shellFeatures.Where(f => f.AlwaysEnabled).ToArray();
        }

        public Task<ShellDescriptor> GetShellDescriptorAsync()
        {
            if (_shellDescriptor == null)
            {
                var configuredFeatures = new ConfiguredFeatures();
                _shellConfiguration.Bind(configuredFeatures);

                var features = _alwaysEnabledFeatures.Concat(configuredFeatures.Features
                    .Select(id => new ShellFeature(id) { AlwaysEnabled = true })).Distinct();

                _shellDescriptor = new ShellDescriptor
                {
                    Features = features.ToList()
                };
            }

            return Task.FromResult(_shellDescriptor);
        }

        public Task UpdateShellDescriptorAsync(int priorSerialNumber, IEnumerable<ShellFeature> enabledFeatures, IEnumerable<ShellParameter> parameters)
        {
            return Task.CompletedTask;
        }

        private class ConfiguredFeatures
        {
            public string[] Features { get; set; } = new string[] { };
        }
    }
}
