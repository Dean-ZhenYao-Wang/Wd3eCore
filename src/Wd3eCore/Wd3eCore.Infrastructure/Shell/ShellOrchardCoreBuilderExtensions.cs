using Microsoft.Extensions.Options;
using Wd3eCore.Environment.Shell;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Environment.Shell.Data.Descriptors;
using Wd3eCore.Environment.Shell.Descriptor;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ShellWd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds services at the host level to load site settings from the file system
        /// and tenant level services to store states and descriptors in the database.
        /// </summary>
        public static Wd3eCoreBuilder AddDataStorage(this Wd3eCoreBuilder builder)
        {
            builder.AddSitesFolder()
                .ConfigureServices(services =>
                {
                    services.AddScoped<IShellDescriptorManager, ShellDescriptorManager>();
                    services.AddScoped<IShellStateManager, ShellStateManager>();
                    services.AddScoped<IShellFeaturesManager, ShellFeaturesManager>();
                    services.AddScoped<IShellDescriptorFeaturesManager, ShellDescriptorFeaturesManager>();
                });

            return builder;
        }

        /// <summary>
        /// Host services to load site settings from the file system
        /// </summary>
        public static Wd3eCoreBuilder AddSitesFolder(this Wd3eCoreBuilder builder)
        {
            var services = builder.ApplicationServices;

            services.AddSingleton<IShellsSettingsSources, ShellsSettingsSources>();
            services.AddSingleton<IShellsConfigurationSources, ShellsConfigurationSources>();
            services.AddSingleton<IShellConfigurationSources, ShellConfigurationSources>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();

            return builder;
        }
    }
}
