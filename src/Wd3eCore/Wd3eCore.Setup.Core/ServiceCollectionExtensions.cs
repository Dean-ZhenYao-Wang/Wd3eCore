using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Setup.Services;

namespace Wd3eCore.Setup
{
    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds tenant level services.
        /// </summary>
        public static IServiceCollection AddSetup(this IServiceCollection services)
        {
            services.AddScoped<ISetupService, SetupService>();

            return services;
        }
    }
}
