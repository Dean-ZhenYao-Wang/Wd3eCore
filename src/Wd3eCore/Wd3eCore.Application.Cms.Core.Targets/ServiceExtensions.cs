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
        /// 在应用程序中添加Wd3e CMS服务。
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

            // Wd3eCoreBuilder不在Wd3eCore.ResourceManagement中，因为它必须独立于Wd3eCore。
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
        ///  在应用程序中添加Wd3e CMS服务，让应用程序通过configure动作改变默认的租户行为和功能集。
        /// </summary>
        public static IServiceCollection AddWd3eCms(this IServiceCollection services, Action<Wd3eCoreBuilder> configure)
        {
            var builder = services.AddWd3eCms();

            configure?.Invoke(builder);

            return services;
        }
    }
}
