using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Wd3eCore.Recipes.Models;

namespace Wd3eCore.Deployment
{
    /// <summary>
    /// 由源代码构建的部署计划的状态。
    /// </summary>
    public class DeploymentPlanResult
    {
        public DeploymentPlanResult(IFileBuilder fileBuilder, RecipeDescriptor recipeDescriptor)
        {
            FileBuilder = fileBuilder;

            Recipe = new JObject();
            Recipe["name"] = recipeDescriptor.Name ?? "";
            Recipe["displayName"] = recipeDescriptor.DisplayName ?? "";
            Recipe["description"] = recipeDescriptor.Description ?? "";
            Recipe["author"] = recipeDescriptor.Author ??  "";
            Recipe["website"] = recipeDescriptor.WebSite ?? "";
            Recipe["version"] = recipeDescriptor.Version ?? "";
            Recipe["issetuprecipe"] = recipeDescriptor.IsSetupRecipe;
            Recipe["categories"] = new JArray(recipeDescriptor.Categories ?? new string[] { });
            Recipe["tags"] = new JArray(recipeDescriptor.Tags ?? new string[] { });
        }

        public JObject Recipe { get; }
        public IList<JObject> Steps { get; } = new List<JObject>();
        public IFileBuilder FileBuilder { get; }
        public async Task FinalizeAsync()
        {
            Recipe["steps"] = new JArray(Steps);

            // 将配方步骤作为其自己的文件内容添加
            await FileBuilder.SetFileAsync("Recipe.json", Encoding.UTF8.GetBytes(Recipe.ToString(Formatting.Indented)));
        }
    }
}
