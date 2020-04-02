using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Wd3eCore.Admin;
using Wd3eCore.Deployment;
using Wd3eCore.Themes.Recipes;
using Wd3eCore.Themes.Services;

namespace Wd3eCore.Themes.Deployment
{
    public class ThemesDeploymentSource : IDeploymentSource
    {
        private readonly ISiteThemeService _siteThemeService;
        private readonly IAdminThemeService _adminThemeService;

        public ThemesDeploymentSource(ISiteThemeService siteThemeService, IAdminThemeService adminThemeService)
        {
            _siteThemeService = siteThemeService;
            _adminThemeService = adminThemeService;
        }

        public async Task ProcessDeploymentStepAsync(DeploymentStep step, DeploymentPlanResult result)
        {
            var themesState = step as ThemesDeploymentStep;

            if (themesState == null)
            {
                return;
            }

            result.Steps.Add(new JObject(
                new JProperty("name", "Themes"),
                new JProperty(nameof(ThemeStepModel.Site), await _siteThemeService.GetCurrentThemeNameAsync()),
                new JProperty(nameof(ThemeStepModel.Admin), await _adminThemeService.GetAdminThemeNameAsync())
            ));
        }
    }
}
