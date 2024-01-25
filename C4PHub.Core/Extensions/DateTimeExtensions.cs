using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    internal static class DateTimeExtensions
    {
        public static string ToUniversalTimeString(this DateTime dateTime)
        {
            string dateFormat = "yyyyMMddTHHmmssZ";
            return dateTime.ToUniversalTime().ToString(dateFormat);
        }

    }
}
