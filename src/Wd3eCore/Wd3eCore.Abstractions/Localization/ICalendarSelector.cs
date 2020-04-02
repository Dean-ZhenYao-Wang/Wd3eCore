using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// Represents a contract for selection a calendar.
    /// </summary>
    public interface ICalendarSelector
    {
        /// <summary>
        /// Gets a calendar.
        /// </summary>
        /// <returns>The selected calendar.</returns>
        Task<CalendarSelectorResult> GetCalendarAsync();
    }
}
