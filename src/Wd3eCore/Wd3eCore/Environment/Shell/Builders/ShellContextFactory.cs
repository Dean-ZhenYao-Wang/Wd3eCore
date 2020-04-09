using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Shell.Descriptor;
using Wd3eCore.Environment.Shell.Descriptor.Models;

namespace Wd3eCore.Environment.Shell.Builders
{
    public class ShellContextFactory : IShellContextFactory
    {
        private readonly ICompositionStrategy _compositionStrategy;
        private readonly IShellContainerFactory _shellContainerFactory;
        private readonly IEnumerable<ShellFeature> _shellFeatures;
        private readonly ILogger _logger;

        public ShellContextFactory(
            ICompositionStrategy compositionStrategy,
            IShellContainerFactory shellContainerFactory,
            IEnumerable<ShellFeature> shellFeatures,
            ILogger<ShellContextFactory> logger)
        {
            _compositionStrategy = compositionStrategy;
            _shellContainerFactory = shellContainerFactory;
            _shellFeatures = shellFeatures;
            _logger = logger;
        }

        async Task<ShellContext> IShellContextFactory.CreateShellContextAsync(ShellSettings settings)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Creating shell context for tenant '{TenantName}'", settings.Name);
                _logger.LogInformation("为租户'{TenantName}'创建shell上下文", settings.Name);
            }

            var describedContext = await CreateDescribedContextAsync(settings, MinimumShellDescriptor());

            ShellDescriptor currentDescriptor;
            using (var scope = describedContext.ServiceProvider.CreateScope())
            {
                var shellDescriptorManager = scope.ServiceProvider.GetService<IShellDescriptorManager>();
                currentDescriptor = await shellDescriptorManager.GetShellDescriptorAsync();
            }

            if (currentDescriptor != null)
            {
                describedContext.Release();
                return await CreateDescribedContextAsync(settings, currentDescriptor);
            }

            return describedContext;
        }

        // TODO: 这应该由一个ISetupService提供，它返回一组ShellFeature实例。
        async Task<ShellContext> IShellContextFactory.CreateSetupContextAsync(ShellSettings settings)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("No shell settings available. Creating shell context for setup", settings.Name);
                _logger.LogDebug("没有可用的shell设置。为设置创建shell上下文", settings.Name);
            }
            var descriptor = MinimumShellDescriptor();

            return await CreateDescribedContextAsync(settings, descriptor);
        }

        public async Task<ShellContext> CreateDescribedContextAsync(ShellSettings settings, ShellDescriptor shellDescriptor)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Creating described context for tenant '{TenantName}'", settings.Name);
                _logger.LogDebug("为租户 '{TenantName_cn}' 创建描述的上下文", settings.Name);
            }

            await settings.EnsureConfigurationAsync();

            var blueprint = await _compositionStrategy.ComposeAsync(settings, shellDescriptor);
            var provider = _shellContainerFactory.CreateContainer(settings, blueprint);

            return new ShellContext
            {
                Settings = settings,
                Blueprint = blueprint,
                ServiceProvider = provider
            };
        }

        /// <summary>
        /// 最小的shell描述符用于引导第一个容器，它将被用来调用所有模块IStartup实现。
        /// 它由模块名称组成，引用了所需场景所需的核心组件。
        /// </summary>
        /// <returns></returns>
        private ShellDescriptor MinimumShellDescriptor()
        {
            // 从DI中注册的ShellFeature实例列表中加载默认功能

            return new ShellDescriptor
            {
                SerialNumber = -1,
                Features = new List<ShellFeature>(_shellFeatures),
                Parameters = new List<ShellParameter>()
            };
        }
    }
}
