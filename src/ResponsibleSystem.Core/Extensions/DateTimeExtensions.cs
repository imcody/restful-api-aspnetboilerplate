using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToMdcDateTimeString(this DateTime date)
        {
            return date.ToString("ddd MMM dd yyyy");
        }

        public static string ToJsDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
