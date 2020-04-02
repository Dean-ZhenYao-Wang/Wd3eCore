using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wd3eCore.Admin;
using Wd3eCore.DisplayManagement.Theming;
using Wd3eCore.Entities;
using Wd3eCore.Settings;
using Wd3eCore.Users.Models;

namespace Wd3eCore.Users.Services
{
    /// <summary>
    /// Provides the theme defined in the site configuration for the current scope (request).
    /// This selector provides AdminTheme as default or fallback for Account|Registration|ResetPassword
    /// controllers based on SiteSettings.
    /// The same <see cref="ThemeSelectorResult"/> is returned if called multiple times
    /// during the same scope.
    /// </summary>
    public class UsersThemeSelector : IThemeSelector
    {
        private readonly ISiteService _siteService;
        private readonly IAdminThemeService _adminThemeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersThemeSelector(
            ISiteService siteService,
            IAdminThemeService adminThemeService,
            IHttpContextAccessor httpContextAccessor)
        {
            _siteService = siteService;
            _adminThemeService = adminThemeService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ThemeSelectorResult> GetThemeAsync()
        {
            var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues;

            if (routeValues["area"]?.ToString() == "Wd3eCore.Users")
            {
                bool useSiteTheme = false;

                switch (routeValues["controller"]?.ToString())
                {
                    case "Account":
                        useSiteTheme = (await _siteService.GetSiteSettingsAsync()).As<LoginSettings>().UseSiteTheme;
                        break;
                    case "Registration":
                        useSiteTheme = (await _siteService.GetSiteSettingsAsync()).As<RegistrationSettings>().UseSiteTheme;
                        break;
                    case "ResetPassword":
                        useSiteTheme = (await _siteService.GetSiteSettingsAsync()).As<ResetPasswordSettings>().UseSiteTheme;
                        break;
                    default:
                        return null;
                }

                string adminThemeName = await _adminThemeService.GetAdminThemeNameAsync();

                if (String.IsNullOrEmpty(adminThemeName))
                {
                    return null;
                }

                return new ThemeSelectorResult
                {
                    Priority = useSiteTheme ? -100 : 100,
                    ThemeName = adminThemeName
                };
            }

            return null;
        }
    }
}
