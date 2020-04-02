using System.Threading.Tasks;
using Wd3eCore.Modules;

namespace Wd3eCore.Settings.Services
{
    /// <summary>
    /// Provides the timezone defined in the site configuration for the current scope (request).
    /// The same <see cref="TimeZoneSelectorResult"/> is returned if called multiple times
    /// during the same scope.
    /// </summary>
    public class DefaultTimeZoneSelector : ITimeZoneSelector
    {
        private readonly ISiteService _siteService;

        public DefaultTimeZoneSelector(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public Task<TimeZoneSelectorResult> GetTimeZoneAsync()
        {
            return Task.FromResult(new TimeZoneSelectorResult
            {
                Priority = 0,
                TimeZoneId = async () => (await _siteService.GetSiteSettingsAsync())?.TimeZoneId
            });
        }
    }
}
