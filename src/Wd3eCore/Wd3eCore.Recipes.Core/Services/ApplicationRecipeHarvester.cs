using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wd3eCore.Environment.Extensions;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Recipes.Services
{
    /// <summary>
    /// Finds recipes in the application content folder.
    /// </summary>
    public class ApplicationRecipeHarvester : RecipeHarvester
    {
        public ApplicationRecipeHarvester(
            IRecipeReader recipeReader,
            IExtensionManager extensionManager,
            IHostEnvironment hostingEnvironment,
            ILogger<RecipeHarvester> logger)
            : base(recipeReader, extensionManager, hostingEnvironment, logger)
        {
        }

        public override Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync()
        {
            return HarvestRecipesAsync("Recipes");
        }
    }
}
