using System;

namespace AEMS.Utilities
{
    public class DateTimeCountry
    {
        public static DateTime DateTimeNow => string.IsNullOrEmpty(AppSettingValues.TimeZoneInfo) ? TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(AppSettingValues.TimeZoneInfo)) : DateTime.UtcNow;

        public static DateTime DateNow => DateTimeNow.Date;

        public static TimeSpan TimeSpanNow => DateTimeNow.TimeOfDay;
    }
}
