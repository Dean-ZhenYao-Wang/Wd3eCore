using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Recipes.Services
{
    public interface IRecipeReader
    {
        Task<RecipeDescriptor> GetRecipeDescriptor(string recipeBasePath, IFileInfo recipeFileInfo, IFileProvider fileProvider);
    }
}
