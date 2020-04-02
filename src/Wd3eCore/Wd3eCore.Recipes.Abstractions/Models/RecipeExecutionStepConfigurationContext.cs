using Newtonsoft.Json.Linq;

namespace Wd3eCore.Recipes.Models
{
    public class RecipeExecutionStepConfigurationContext : ConfigurationContext
    {
        public RecipeExecutionStepConfigurationContext(JObject configurationElement) : base(configurationElement)
        {
        }
    }
}
