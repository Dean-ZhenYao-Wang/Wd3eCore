using Microsoft.Extensions.DependencyInjection;
using Wd3eCore.Recipes.Services;

namespace Wd3eCore.Recipes
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecipes(this IServiceCollection services)
        {
            services.AddScoped<IRecipeHarvester, ApplicationRecipeHarvester>();
            services.AddScoped<IRecipeHarvester, RecipeHarvester>();
            services.AddSingleton<IRecipeExecutor, RecipeExecutor>();
            services.AddScoped<IRecipeMigrator, RecipeMigrator>();
            services.AddScoped<IRecipeReader, RecipeReader>();

            return services;
        }
    }
}
