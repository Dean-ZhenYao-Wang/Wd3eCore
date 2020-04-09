using System.Threading.Tasks;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 为当前请求提供时区。
    /// </summary>
    public interface ITimeZoneSelector
    {
        Task<TimeZoneSelectorResult> GetTimeZoneAsync();
    }
}
