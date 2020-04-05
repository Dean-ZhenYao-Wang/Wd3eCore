using System;
using Wd3eCore.ResourceManagement.TagHelpers;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds Wd3e CMS services to the application.
        /// </summary>
        public static Wd3eCoreBuilder AddWd3eCms(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var builder = services.AddWd3eCore()

                .AddCommands()

                .AddMvc()
                .AddEmailAddressValidator()
                .AddSetupFeatures("Wd3eCore.Setup")

                .AddDataAccess()
                .AddDataStorage()
                .AddBackgroundService()

                .AddTheming()
                //.AddLiquidViews()
                .AddCaching();

            // Wd3eCoreBuilder is not available in Wd3eCore.ResourceManagement as it has to
            // remain independent from Wd3eCore.
            builder.ConfigureServices(s =>
            {
                s.AddResourceManagement();

                s.AddTagHelpers<LinkTagHelper>();
                s.AddTagHelpers<MetaTagHelper>();
                s.AddTagHelpers<ResourcesTagHelper>();
                s.AddTagHelpers<ScriptTagHelper>();
                s.AddTagHelpers<StyleTagHelper>();
            });

            return builder;
        }

        /// <summary>
        /// Adds Wd3e CMS services to the application and let the app change the
        /// default tenant behavior and set of features through a configure action.
        /// </summary>
        public static IServiceCollection AddWd3eCms(this IServiceCollection services, Action<Wd3eCoreBuilder> configure)
        {
            var builder = services.AddWd3eCms();

            configure?.Invoke(builder);

            return services;
        }
    }
}
