using System.Globalization;
using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示<see cref="ICalendarSelector"/>的默认实现方式
    /// </summary>
    public class DefaultCalendarSelector : ICalendarSelector
    {
        private static readonly Task<CalendarSelectorResult> CalendarResult =
            Task.FromResult(new CalendarSelectorResult
            {
                Priority = 0,
                CalendarName = () =>
                {
                    return Task.FromResult(BclCalendars.GetCalendarName(CultureInfo.CurrentUICulture.Calendar));
                }
            });

        /// <inheritdocs />
        public Task<CalendarSelectorResult> GetCalendarAsync()
        {
            return CalendarResult;
        }
    }
}
