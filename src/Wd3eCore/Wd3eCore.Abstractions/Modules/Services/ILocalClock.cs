using System;
using System.Threading.Tasks;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 提供当前时间和时区的本地值。
    /// </summary>
    public interface ILocalClock
    {
        /// <summary>
        /// 获取本地时区的时间。
        /// </summary>
        Task<DateTimeOffset> LocalNowAsync { get; }

        /// <summary>
        /// 返回本地时区。
        /// </summary>
        Task<ITimeZone> GetLocalTimeZoneAsync();

        /// <summary>
        /// 将<see cref="DateTimeOffset" />转换为指定的<see cref="ITimeZone" />实例。
        /// </summary>
        Task<DateTimeOffset> ConvertToLocalAsync(DateTimeOffset dateTimeOffset);

        /// <summary>
        /// 将表示本地时间的<see cref="DateTime" />转换为UTC值。
        /// </summary>
        Task<DateTime> ConvertToUtcAsync(DateTime dateTime);
    }
}
