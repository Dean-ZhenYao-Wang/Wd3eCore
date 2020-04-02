using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Recipes.Services;

namespace Wd3eCore.Recipes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecipeExecutionStep<TImplementation>(
            this IServiceCollection serviceCollection)
            where TImplementation : class, IRecipeStepHandler
        {
            serviceCollection.AddScoped<IRecipeStepHandler, TImplementation>();

            return serviceCollection;
        }
    }
}
