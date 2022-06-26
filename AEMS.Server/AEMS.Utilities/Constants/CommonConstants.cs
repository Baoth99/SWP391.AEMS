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
    
    public class DeviceState
    {
        public const int New = 0;
        public const int InUse = 1;
    }

    public class RemoteControlAction
    {
        public const string AccOn = "ACC_ON";
        public const string AccOff = "ACC_OFF";

        public const string BatteryReportOn = "BATTERY_REPORT_ON";
        public const string BatteryReportOff = "BATTERY_REPORT_OFF";
        public const string TemperatureReportOn = "TEMPERATURE_REPORT_ON";
        public const string TemperatureReportOff = "TEMPERATURE_REPORT_OFF";
    }
    public static class ContentTypeString
    {
        public const string ExcelFileContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;";
    }
}
