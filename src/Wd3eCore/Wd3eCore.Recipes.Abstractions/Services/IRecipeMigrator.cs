using System.Threading.Tasks;
using Wd3eCore.Data.Migration;

namespace Wd3eCore.Recipes.Services
{
    public interface IRecipeMigrator
    {
        Task<string> ExecuteAsync(string recipeFileName, IDataMigration migration);
    }
}
