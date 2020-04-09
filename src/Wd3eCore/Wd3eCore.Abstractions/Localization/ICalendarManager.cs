using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示用于管理日历的契约。
    /// </summary>
    public interface ICalendarManager
    {
        /// <summary>
        /// 获取当前日历。
        /// </summary>
        /// <returns>当前日历名。</returns>
        Task<CalendarName> GetCurrentCalendar();
    }
}
