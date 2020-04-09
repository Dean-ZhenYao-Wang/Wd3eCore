using System;
using System.Threading.Tasks;

namespace Wd3eCore.Modules
{
    public static class ClockExtensions
    {
        /// <summary>
        /// 将<see cref="DateTime" />转换为指定的<see cref="ITimeZone" />实例。
        /// </summary>
        public static DateTimeOffset ConvertToTimeZone(this IClock clock, DateTime dateTime, ITimeZone timeZone)
        {
            DateTime dateTimeUtc;
            switch (dateTime.Kind)
            {
                case DateTimeKind.Utc:
                    dateTimeUtc = dateTime;
                    break;
                case DateTimeKind.Local:
                    dateTimeUtc = dateTime.ToUniversalTime();
                    break;
                default: //DateTimeKind.Unspecified
                    dateTimeUtc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    break;
            }

            return clock.ConvertToTimeZone(new DateTimeOffset(dateTimeUtc), timeZone);
        }

        /// <summary>
        /// 将<see cref="DateTime" />转换为指定的<see cref="ITimeZone" />实例。
        /// </summary>
        public static Task<DateTimeOffset> ConvertToLocalAsync(this ILocalClock localClock, DateTime dateTime)
        {
            DateTime dateTimeUtc;
            switch (dateTime.Kind)
            {
                case DateTimeKind.Utc:
                    dateTimeUtc = dateTime;
                    break;
                case DateTimeKind.Local:
                    dateTimeUtc = dateTime.ToUniversalTime();
                    break;
                default: //DateTimeKind.Unspecified
                    dateTimeUtc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    break;
            }

            return localClock.ConvertToLocalAsync(new DateTimeOffset(dateTimeUtc));
        }
    }
}
