using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Apis.GraphQL;
using Wd3eCore.Modules;

namespace Wd3eCore.Localization.GraphQL
{
    /// <summary>
    /// Represents the localization module entry point for Graph QL.
    /// </summary>
    [RequireFeatures("Wd3eCore.Apis.GraphQL")]
    public class Startup : StartupBase
    {
        /// <inheritdocs />
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISchemaBuilder, SiteCulturesQuery>();
            services.AddTransient<CultureQueryObjectType>();
        }
    }
}
