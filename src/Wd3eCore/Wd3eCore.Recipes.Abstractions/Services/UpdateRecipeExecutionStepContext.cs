using Newtonsoft.Json.Linq;

namespace Wd3eCore.Recipes.Services
{
    public class UpdateRecipeExecutionStepContext
    {
        public JObject RecipeDocument { get; set; }
        public JObject Step { get; set; }
    }
}
