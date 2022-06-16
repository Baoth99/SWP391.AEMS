using System;

namespace AEMS.Utilities
{
    public class DateTimeCountry
    {
        public static DateTime DateTimeNow => string.IsNullOrEmpty(AppSettingValues.TimeZoneInfo) ? TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(AppSettingValues.TimeZoneInfo)) : DateTime.UtcNow;

        public static DateTime DateNow => DateTimeNow.Date;

        public static TimeSpan TimeSpanNow => DateTimeNow.TimeOfDay;
    }


    public class DeviceStatusConst
    {
        public const int Connected = 0;
        public const int Standby = 1;
        public const int Disconnected = 2;
    }

    public class DeviceStatusMesage
    {
        public const string Connected = "Connected";
        public const string Standby = "Standby";
        public const string Disconnected = "Disconnected";
    }
    
}
