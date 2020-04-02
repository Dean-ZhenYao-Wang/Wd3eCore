using System;
using System.Threading.Tasks;
using Wd3eCore.DisplayManagement.Theming;

namespace Wd3eCore.Themes.Services
{
    /// <summary>
    /// Provides the theme defined in the site configuration for the current scope (request).
    /// The same <see cref="ThemeSelectorResult"/> is returned if called multiple times
    /// during the same scope.
    /// </summary>
    public class SiteThemeSelector : IThemeSelector
    {
        private readonly ISiteThemeService _siteThemeService;

        public SiteThemeSelector(ISiteThemeService siteThemeService)
        {
            _siteThemeService = siteThemeService;
        }

        public async Task<ThemeSelectorResult> GetThemeAsync()
        {
            string currentThemeName = await _siteThemeService.GetCurrentThemeNameAsync();
            if (String.IsNullOrEmpty(currentThemeName))
            {
                return null;
            }

            return new ThemeSelectorResult
            {
                Priority = 0,
                ThemeName = currentThemeName
            };
        }
    }
}
