using System.Collections.Generic;
using System.Threading.Tasks;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Setup.Services
{
    /// <summary>
    /// Contract that represents the setup service.
    /// </summary>
    public interface ISetupService
    {
        /// <summary>
        /// Gets the setup recipes.
        /// </summary>
        /// <returns>A list of <see cref="RecipeDescriptor"/>s.</returns>
        Task<IEnumerable<RecipeDescriptor>> GetSetupRecipesAsync();

        /// <summary>
        /// Set up the tenant.
        /// </summary>
        /// <param name="context">The <see cref="SetupContext"/>.</param>
        /// <returns> A GUID string the represents a setup execution identifier.</returns>
        Task<string> SetupAsync(SetupContext context);
    }
}
