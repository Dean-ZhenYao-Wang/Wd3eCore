using System.Threading.Tasks;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// Provides the timezone for the current request.
    /// </summary>
    public interface ITimeZoneSelector
    {
        Task<TimeZoneSelectorResult> GetTimeZoneAsync();
    }
}
