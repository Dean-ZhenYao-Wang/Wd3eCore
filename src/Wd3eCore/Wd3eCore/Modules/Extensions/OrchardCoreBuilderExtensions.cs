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
        /// Registers at the host level a set of features which are always enabled for any tenant.
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
        /// Registers at the tenant level a set of features which are always enabled.
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
        /// Registers a default tenant with a set of features that are used to setup and configure the actual tenants.
        /// For instance you can use this to add a custom Setup module.
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
        /// Registers tenants defined in configuration.
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
        /// Registers a single tenant with the specified set of features.
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
        /// Registers and configures a background hosted service to manage tenant background tasks.
        /// </summary>
        public static Wd3eCoreBuilder AddBackgroundService(this Wd3eCoreBuilder builder)
        {
            builder.ApplicationServices.AddSingleton<IHostedService, ModularBackgroundService>();

            return builder;
        }
    }
}
