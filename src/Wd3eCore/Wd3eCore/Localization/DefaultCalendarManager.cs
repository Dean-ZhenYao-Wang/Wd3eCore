using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// Represents a default implementation to manage the calendars.
    /// </summary>
    public class DefaultCalendarManager : ICalendarManager
    {
        private readonly IEnumerable<ICalendarSelector> _calendarSelectors;

        private CalendarName? _calendarName;

        /// <summary>
        /// 创建一个新的 <see cref="DefaultCalendarManager"/>实例
        /// </summary>
        /// <param name="calendarSelectors"><see cref="ICalendarSelector"/>的列表</param>
        public DefaultCalendarManager(IEnumerable<ICalendarSelector> calendarSelectors)
        {
            _calendarSelectors = calendarSelectors;
        }

        /// <inheritdocs />
        public async Task<CalendarName> GetCurrentCalendar()
        {
            if (_calendarName.HasValue)
            {
                return _calendarName.Value;
            }

            var calendarResults = new List<CalendarSelectorResult>();

            foreach (var calendarSelector in _calendarSelectors)
            {
                var calendarResult = await calendarSelector.GetCalendarAsync();

                if (calendarResult != null)
                {
                    calendarResults.Add(calendarResult);
                }
            }

            if (calendarResults.Count == 0)
            {
                return CalendarName.Unknown;
            }
            else if (calendarResults.Count > 1)
            {
                calendarResults.Sort((x, y) => y.Priority.CompareTo(x.Priority));
            }

            _calendarName = await calendarResults.First().CalendarName();

            return _calendarName.Value;
        }
    }
}
