using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Environment.Shell.Descriptor;
using Wd3eCore.Environment.Shell.Descriptor.Models;
using Wd3eCore.Environment.Shell.Descriptor.Settings;
using Wd3eCore.Modules;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// 在主机级注册一组始终为任何租户启用的特性。
        /// </summary>
        public static Wd3eCoreBuilder AddGlobalFeatures(this Wd3eCoreBuilder builder, params string[] featureIds)
        {
            foreach (var featureId in featureIds)
            {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId, alwaysEnabled: true));
            }

            return builder;
        }

        /// <summary>
        /// 在租户级别注册一组始终启用的特性。
        /// </summary>
        public static Wd3eCoreBuilder AddTenantFeatures(this Wd3eCoreBuilder builder, params string[] featureIds)
        {
            builder.ConfigureServices(services =>
            {
                foreach (var featureId in featureIds)
                {
                    services.AddTransient(sp => new ShellFeature(featureId, alwaysEnabled: true));
                }
            });

            return builder;
        }

        /// <summary>
        /// 使用一组用于设置和配置实际租户的特性注册默认租户。
        /// 例如，可以使用它来添加自定义设置模块。
        /// </summary>
        public static Wd3eCoreBuilder AddSetupFeatures(this Wd3eCoreBuilder builder, params string[] featureIds)
        {
            foreach (var featureId in featureIds)
            {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId));
            }

            return builder;
        }

        /// <summary>
        /// 注册配置中定义的租户。
        /// </summary>
        public static Wd3eCoreBuilder WithTenants(this Wd3eCoreBuilder builder)
        {
            var services = builder.ApplicationServices;

            services.AddSingleton<IShellsSettingsSources, ShellsSettingsSources>();
            services.AddSingleton<IShellsConfigurationSources, ShellsConfigurationSources>();
            services.AddSingleton<IShellConfigurationSources, ShellConfigurationSources>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();

            return builder.ConfigureServices(s =>
            {
                s.AddScoped<IShellDescriptorManager, ConfiguredFeaturesShellDescriptorManager>();
            });
        }

        /// <summary>
        /// 使用指定的特性集注册单个租户。
        /// </summary>
        public static Wd3eCoreBuilder WithFeatures(this Wd3eCoreBuilder builder, params string[] featureIds)
        {
            foreach (var featureId in featureIds)
            {
                builder.ApplicationServices.AddTransient(sp => new ShellFeature(featureId));
            }

            builder.ApplicationServices.AddSetFeaturesDescriptor();

            return builder;
        }

        /// <summary>
        /// 注册并配置后台托管服务，以管理租户后台任务。
        /// </summary>
        public static Wd3eCoreBuilder AddBackgroundService(this Wd3eCoreBuilder builder)
        {
            builder.ApplicationServices.AddSingleton<IHostedService, ModularBackgroundService>();

            return builder;
        }
    }
}
