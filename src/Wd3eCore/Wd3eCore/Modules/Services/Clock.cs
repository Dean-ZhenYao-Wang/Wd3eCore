using System;
using System.Linq;
using NodaTime;
using NodaTime.TimeZones;

namespace Wd3eCore.Modules
{
    public class Clock : IClock
    {
        private static Instant CurrentInstant => SystemClock.Instance.GetCurrentInstant();

        /// <summary>
        /// 返回 "现在 "的Datetime Kind.Utc。
        /// </summary>
        /// <inheritdoc />
        public DateTime UtcNow => CurrentInstant.ToDateTimeUtc();

        /// <summary>
        /// 以ITimeZone[]的形式返回一个有效的时区列表，其中key为时区id(string)，值可用于显示。
        /// 该列表经过过滤，只包含对当前和近期的真实地点合理有效的选择。
        /// 该列表也会先按UTC偏移量排序，然后按时区名称排序。
        /// </summary>
        public ITimeZone[] GetTimeZones()
        {
            var list =
                from location in TzdbDateTimeZoneSource.Default.ZoneLocations
                let zoneId = location.ZoneId
                let tz = DateTimeZoneProviders.Tzdb[zoneId]
                let zoneInterval = tz.GetZoneInterval(CurrentInstant)
                orderby zoneInterval.StandardOffset, zoneId
                select new TimeZone(zoneId, zoneInterval.StandardOffset, zoneInterval.WallOffset, tz);

            return list.ToArray();
        }

        public ITimeZone GetTimeZone(string timeZoneId)
        {
            if (String.IsNullOrEmpty(timeZoneId))
            {
                return GetSystemTimeZone();
            }

            var dateTimeZone = GetDateTimeZone(timeZoneId);

            return CreateTimeZone(dateTimeZone);
        }

        public ITimeZone GetSystemTimeZone()
        {
            var timezone = DateTimeZoneProviders.Tzdb.GetSystemDefault();
            if (TzdbDateTimeZoneSource.Default.CanonicalIdMap.ContainsKey(timezone.Id))
            {
                var canonicalTimeZoneId = TzdbDateTimeZoneSource.Default.CanonicalIdMap[timezone.Id];
                timezone = GetDateTimeZone(canonicalTimeZoneId);
            }
            return CreateTimeZone(timezone);
        }

        public DateTimeOffset ConvertToTimeZone(DateTimeOffset dateTimeOffSet, ITimeZone timeZone)
        {
            var offsetDateTime = OffsetDateTime.FromDateTimeOffset(dateTimeOffSet);
            return offsetDateTime.InZone(((TimeZone)timeZone).DateTimeZone).ToDateTimeOffset();
        }

        internal static DateTimeZone GetDateTimeZone(string timeZone)
        {
            if (!String.IsNullOrEmpty(timeZone) && IsValidTimeZone(DateTimeZoneProviders.Tzdb, timeZone))
            {
                return DateTimeZoneProviders.Tzdb[timeZone];
            }

            return DateTimeZoneProviders.Tzdb.GetSystemDefault();
        }

        private ITimeZone CreateTimeZone(DateTimeZone dateTimeZone)
        {
            if (dateTimeZone == null)
            {
                throw new ArgumentException(nameof(DateTimeZone));
            }

            var zoneInterval = dateTimeZone.GetZoneInterval(CurrentInstant);

            return new TimeZone(dateTimeZone.Id, zoneInterval.StandardOffset, zoneInterval.WallOffset, dateTimeZone);
        }

        private static bool IsValidTimeZone(IDateTimeZoneProvider provider, string timeZoneId)
        {
            return provider.GetZoneOrNull(timeZoneId) != null;
        }
    }
}
