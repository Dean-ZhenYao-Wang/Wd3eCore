using Microsoft.Extensions.DependencyInjection.Extensions;
using Wd3eCore.Environment.Shell.Configuration;
using Wd3eCore.Shells.Database.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DatabaseShellsWd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Host services to load shells settings and configuration from database.
        /// </summary>
        public static Wd3eCoreBuilder AddDatabaseShellsConfiguration(this Wd3eCoreBuilder builder)
        {
            var services = builder.ApplicationServices;

            services.Replace(ServiceDescriptor.Singleton<IShellsSettingsSources, DatabaseShellsSettingsSources>());
            services.Replace(ServiceDescriptor.Singleton<IShellConfigurationSources, DatabaseShellConfigurationSources>());

            return builder;
        }
    }
}
