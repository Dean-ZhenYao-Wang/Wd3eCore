using System;
using System.Threading.Tasks;
using Wd3eCore.Admin;
using Wd3eCore.Recipes.Models;
using Wd3eCore.Recipes.Services;
using Wd3eCore.Themes.Services;

namespace Wd3eCore.Themes.Recipes
{
    /// <summary>
    /// This recipe step defines the site and admin current themes.
    /// </summary>
    public class ThemesStep : IRecipeStepHandler
    {
        private readonly ISiteThemeService _siteThemeService;
        private readonly IAdminThemeService _adminThemeService;

        public ThemesStep(
            ISiteThemeService siteThemeService,
            IAdminThemeService adminThemeService)
        {
            _adminThemeService = adminThemeService;
            _siteThemeService = siteThemeService;
        }

        public async Task ExecuteAsync(RecipeExecutionContext context)
        {
            if (!String.Equals(context.Name, "Themes", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var model = context.Step.ToObject<ThemeStepModel>();

            if (!String.IsNullOrEmpty(model.Site))
            {
                await _siteThemeService.SetSiteThemeAsync(model.Site);
            }

            if (!String.IsNullOrEmpty(model.Admin))
            {
                await _adminThemeService.SetAdminThemeAsync(model.Admin);
            }
        }
    }

    public class ThemeStepModel
    {
        public string Site { get; set; }
        public string Admin { get; set; }
    }
}
