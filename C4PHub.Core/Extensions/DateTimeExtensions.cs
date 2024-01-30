using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DateTimeExtensions
    {
        public static string ToTimeFormatString(this DateTime dateTime)
        {
            string dateFormat = "yyyyMMddTHHmmssZ";
            return dateTime.ToUniversalTime().ToString(dateFormat);
        }

        public static string ToStandardFormatString(this DateTimeOffset dateTime)
        {
            if (dateTime.Offset == TimeSpan.Zero)
            {
                return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else
            {
                return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz");
            }
        }

        public static string ToUniversalFormatString(this DateTimeOffset dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
        }

    }
}
