using System;

namespace Wd3eCore.Modules
{
    /// <summary>
    /// 提供当前Utc<see cref="DateTime"/>，以及与时区相关的方法。
    /// 只要需要当前日期和时间，就应该使用此服务，而不是直接使用<seealso cref="DateTime"/>。
    /// 如果需要本地日期时间和时区，可以使用<see cref="ILocalClock" />。
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// 获取系统的当前<see cref="DateTime"/>，用Utc表示
        /// </summary>
        /// <remarks>
        /// <see cref="DateTime"/>作为这个属性通常用于在UTC中存储当前日期时间，<see cref="DateTimeOffset" />会影响可用性。
        /// </remarks>
        DateTime UtcNow { get; }

        /// <summary>
        /// 返回所有可用的<see cref="ITimeZone" />.
        /// </summary>
        ITimeZone[] GetTimeZones();

        /// <summary>
        ///  返回一个时区ID的 <see cref="ITimeZone" />，如果没有找到，则返回本地系统的时区ID。
        /// </summary>
        ITimeZone GetTimeZone(string timeZoneId);

        /// <summary>
        /// 返回系统的默认<see cref="ITimeZone" />。
        /// </summary>
        ITimeZone GetSystemTimeZone();

        /// <summary>
        /// 将<see cref="DateTimeOffset" />转换为指定的<see cref="ITimeZone" />实例。
        /// </summary>
        DateTimeOffset ConvertToTimeZone(DateTimeOffset dateTimeOffset, ITimeZone timeZone);
    }
}
