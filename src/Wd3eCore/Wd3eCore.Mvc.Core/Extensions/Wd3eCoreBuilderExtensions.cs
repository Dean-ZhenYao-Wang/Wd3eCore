using Microsoft.AspNetCore.Routing;
using Wd3eCore.Mvc;
using Wd3eCore.Routing;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds tenant level MVC services and configuration.
        /// </summary>
        public static Wd3eCoreBuilder AddMvc(this Wd3eCoreBuilder builder)
        {
            builder.ConfigureServices(collection =>
            {
                // Allows a tenant to add its own route endpoint schemes for link generation.
                collection.AddSingleton<IEndpointAddressScheme<RouteValuesAddress>, ShellRouteValuesAddressScheme>();
            },
            // Need to be registered last.
            order: int.MaxValue - 100);

            return builder.RegisterStartup<Startup>();
        }
    }
}
