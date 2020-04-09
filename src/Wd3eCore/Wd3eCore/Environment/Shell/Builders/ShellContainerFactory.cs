using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Environment.Extensions.Features;
using Wd3eCore.Environment.Shell.Builders.Models;
using Wd3eCore.Modules;

namespace Wd3eCore.Environment.Shell.Builders
{
    public class ShellContainerFactory : IShellContainerFactory
    {
        private IFeatureInfo _applicationFeature;

        private readonly IHostEnvironment _hostingEnvironment;
        private readonly IExtensionManager _extensionManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _applicationServices;

        public ShellContainerFactory(
            IHostEnvironment hostingEnvironment,
            IExtensionManager extensionManager,
            IServiceProvider serviceProvider,
            IServiceCollection applicationServices)
        {
            _hostingEnvironment = hostingEnvironment;
            _extensionManager = extensionManager;
            _applicationServices = applicationServices;
            _serviceProvider = serviceProvider;
        }

        public void AddCoreServices(IServiceCollection services)
        {
            services.TryAddScoped<IShellStateUpdater, ShellStateUpdater>();
            services.TryAddScoped<IShellStateManager, NullShellStateManager>();
            services.AddScoped<IShellDescriptorManagerEventHandler, ShellStateCoordinator>();
        }

        public IServiceProvider CreateContainer(ShellSettings settings, ShellBlueprint blueprint)
        {
            var tenantServiceCollection = _serviceProvider.CreateChildContainer(_applicationServices);

            tenantServiceCollection.AddSingleton(settings);
            tenantServiceCollection.AddSingleton(sp =>
            {
                //延迟解决，因为它的构造是延迟的
                var shellSettings = sp.GetRequiredService<ShellSettings>();
                return shellSettings.ShellConfiguration;
            });

            tenantServiceCollection.AddSingleton(blueprint.Descriptor);
            tenantServiceCollection.AddSingleton(blueprint);

            AddCoreServices(tenantServiceCollection);

            // 执行IStartup注册

            var moduleServiceCollection = _serviceProvider.CreateChildContainer(_applicationServices);

            foreach (var dependency in blueprint.Dependencies.Where(t => typeof(IStartup).IsAssignableFrom(t.Key)))
            {
                moduleServiceCollection.AddSingleton(typeof(IStartup), dependency.Key);
                tenantServiceCollection.AddSingleton(typeof(IStartup), dependency.Key);
            }

            // 为了不在由'ShellHost'完成之前，触发特性加载，
            // 在这里启动应用程序的特性，而不是在构造函数中完成。
            EnsureApplicationFeature();

            foreach (var rawStartup in blueprint.Dependencies.Keys.Where(t => t.Name == "Startup"))
            {
                //继承自IStartup的Startup类已被处理
                if (typeof(IStartup).IsAssignableFrom(rawStartup))
                {
                    continue;
                }

                // 忽略主应用程序中的启动类
                if (blueprint.Dependencies.TryGetValue(rawStartup, out var startupFeature) && startupFeature.FeatureInfo.Id == _applicationFeature.Id)
                {
                    continue;
                }

                // 围绕此方法创建一个包装器
                var configureServicesMethod = rawStartup.GetMethod(
                    nameof(IStartup.ConfigureServices),
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    CallingConventions.Any,
                    new Type[] { typeof(IServiceCollection) },
                    null);

                var configureMethod = rawStartup.GetMethod(
                    nameof(IStartup.Configure),
                    BindingFlags.Public | BindingFlags.Instance);

                var orderProperty = rawStartup.GetProperty(
                    nameof(IStartup.Order),
                    BindingFlags.Public | BindingFlags.Instance);

                var configureOrderProperty = rawStartup.GetProperty(
                    nameof(IStartup.ConfigureOrder),
                    BindingFlags.Public | BindingFlags.Instance);

                //将startup类添加到DI中，这样我们可以用有效的ctor参数实例化它
                moduleServiceCollection.AddSingleton(rawStartup);
                tenantServiceCollection.AddSingleton(rawStartup);

                moduleServiceCollection.AddSingleton<IStartup>(sp =>
                {
                    var startupInstance = sp.GetService(rawStartup);
                    return new StartupBaseMock(startupInstance, configureServicesMethod, configureMethod, orderProperty, configureOrderProperty);
                });

                tenantServiceCollection.AddSingleton<IStartup>(sp =>
                {
                    var startupInstance = sp.GetService(rawStartup);
                    return new StartupBaseMock(startupInstance, configureServicesMethod, configureMethod, orderProperty, configureOrderProperty);
                });
            }

            // 向模块提供shell设置
            moduleServiceCollection.AddSingleton(settings);
            moduleServiceCollection.AddSingleton(sp =>
            {
                // 延迟解析，因为它的构造是延迟的
                var shellSettings = sp.GetRequiredService<ShellSettings>();
                return shellSettings.ShellConfiguration;
            });

            var moduleServiceProvider = moduleServiceCollection.BuildServiceProvider(true);

            // 按特征ID索引所有的服务描述符
            var featureAwareServiceCollection = new FeatureAwareServiceCollection(tenantServiceCollection);

            var startups = moduleServiceProvider.GetServices<IStartup>();

            // IStartup实例按模块依赖关系排序，默认的顺序为0。
            // OrderBy执行一个稳定的排序，因此在相同的Order值中保留顺序。
            startups = startups.OrderBy(s => s.Order);

            // 让任何模块在租户中添加自定义服务描述符。
            foreach (var startup in startups)
            {
                var feature = blueprint.Dependencies.FirstOrDefault(x => x.Key == startup.GetType()).Value?.FeatureInfo;

                // 如果启动不是来自于扩展，则将其与应用特性关联起来。
                // 例如，当Startup类在应用程序中用Configure<Startup>()注册时。
                featureAwareServiceCollection.SetCurrentFeature(feature ?? _applicationFeature);
                startup.ConfigureServices(featureAwareServiceCollection);
            }

            (moduleServiceProvider as IDisposable).Dispose();

            var shellServiceProvider = tenantServiceCollection.BuildServiceProvider(true);

            // 在ITypeFeatureProvider中注册所有的DIed类型。
            var typeFeatureProvider = shellServiceProvider.GetRequiredService<ITypeFeatureProvider>();

            foreach (var featureServiceCollection in featureAwareServiceCollection.FeatureCollections)
            {
                foreach (var serviceDescriptor in featureServiceCollection.Value)
                {
                    var type = serviceDescriptor.GetImplementationType();

                    if (type != null)
                    {
                        var feature = featureServiceCollection.Key;

                        if (feature == _applicationFeature)
                        {
                            var attribute = type.GetCustomAttributes<FeatureAttribute>(false).FirstOrDefault();

                            if (attribute != null)
                            {
                                feature = featureServiceCollection.Key.Extension.Features
                                    .FirstOrDefault(f => f.Id == attribute.FeatureName)
                                    ?? feature;
                            }
                        }

                        typeFeatureProvider.TryAdd(type, feature);
                    }
                }
            }

            return shellServiceProvider;
        }

        private void EnsureApplicationFeature()
        {
            if (_applicationFeature == null)
            {
                lock (this)
                {
                    if (_applicationFeature == null)
                    {
                        _applicationFeature = _extensionManager.GetFeatures()
                            .FirstOrDefault(f => f.Id == _hostingEnvironment.ApplicationName);
                    }
                }
            }
        }
    }
}
