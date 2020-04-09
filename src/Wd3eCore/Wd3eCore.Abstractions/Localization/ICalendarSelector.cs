using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示用于选择日历的契约。
    /// </summary>
    public interface ICalendarSelector
    {
        /// <summary>
        /// 一个日历。
        /// </summary>
        /// <returns>选择的日历。</returns>
        Task<CalendarSelectorResult> GetCalendarAsync();
    }
}
