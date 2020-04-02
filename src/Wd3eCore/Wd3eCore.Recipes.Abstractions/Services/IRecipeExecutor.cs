using System.Threading;
using System.Threading.Tasks;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Recipes.Services
{
    public interface IRecipeExecutor
    {
        Task<string> ExecuteAsync(string executionId, RecipeDescriptor recipeDescriptor, object environment, CancellationToken cancellationToken);
    }
}
