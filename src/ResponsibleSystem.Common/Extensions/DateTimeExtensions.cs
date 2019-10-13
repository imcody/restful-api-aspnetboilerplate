using System;

namespace ResponsibleSystem.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToAppDateTimeString(this DateTime date)
        {
            return date.ToString("ddd MMM dd yyyy");
        }

        public static string ToAppDateTimeWithTimeString(this DateTime date)
        {
            return date.ToString("ddd MMM dd yyyy, HH:mm");
        }

        public static string ToJsDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
