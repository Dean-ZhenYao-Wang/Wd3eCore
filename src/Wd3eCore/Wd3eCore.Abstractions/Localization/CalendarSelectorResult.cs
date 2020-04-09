using System;
using System.Threading.Tasks;

namespace Wd3eCore.Localization
{
    /// <summary>
    /// 表示所选日历的结果。
    /// </summary>
    public class CalendarSelectorResult
    {
        /// <summary>
        /// 获取或设置优先级。
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 获取或设置日历名称。
        /// </summary>
        public Func<Task<CalendarName>> CalendarName { get; set; }
    }
}
