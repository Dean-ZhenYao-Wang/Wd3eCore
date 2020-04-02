using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Recipes.Services
{
    public interface IRecipeHarvester
    {
        /// <summary>
        /// Returns a collection of all recipes.
        /// </summary>
        Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync();
    }
}
