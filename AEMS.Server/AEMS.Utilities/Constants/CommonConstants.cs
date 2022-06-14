using System;

namespace AEMS.Utilities
{
    public class DateTimeCountry
    {
        public static DateTime DateTimeNow => string.IsNullOrEmpty(AppSettingValues.TimeZoneInfo) ? TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(AppSettingValues.TimeZoneInfo)) : DateTime.UtcNow;

        public static DateTime DateNow => DateTimeNow.Date;

        public static TimeSpan TimeSpanNow => DateTimeNow.TimeOfDay;
    }

    public class EquipmentStatus
    {
        public const int InActive = 0;
        public const int Active = 1;
        public const int Maintained = 2;
        public const int Broken = 3;
    }


}
