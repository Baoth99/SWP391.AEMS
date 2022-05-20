using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEMS.Simulator.Core
{
    public class DateTimeUtils
    {
    }

    public class DateTimeCountry
    {
        private static DateTime GetDateTimeNow(string timeZoneInfo) => !string.IsNullOrEmpty(timeZoneInfo) ? TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfo)) : DateTime.UtcNow;

        public static DateTime DateTimeNow(string timeZoneInfo = "SE Asia Standard Time") => GetDateTimeNow(timeZoneInfo);

        public static DateTime DateNow(string timeZoneInfo = "SE Asia Standard Time") => GetDateTimeNow(timeZoneInfo).Date;

        public static TimeSpan TimeSpanNow(string timeZoneInfo = "SE Asia Standard Time") => GetDateTimeNow(timeZoneInfo).TimeOfDay;
    }
}
