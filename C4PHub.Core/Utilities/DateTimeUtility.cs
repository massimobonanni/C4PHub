using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Utilities
{
    public static class DateTimeUtility
    {
        private static string[] supportedDateTimeFormats = new string[]
            {
                    "d MMM yyyy",
                    "d MMM yyyy h:mm tt",
                    "d MMM yyyy h:mm tt zzz",
                    "d MMM yyyy h:mm tt 'UTC'zzz",
                    "ddd, dd MMM yyyy HH:mm UTCzzz",
                    "ddd, dd MMM yyyy HH:mm UTC|zzz",
                    "dd/MM/yyyy"
            };

        public static DateTimeOffset? ParseStringToDateTimeOffset(string input)
        {
            if (DateTimeOffset.TryParseExact(input, supportedDateTimeFormats, CultureInfo.InvariantCulture,
                DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowWhiteSpaces, out DateTimeOffset result))
            {
                return result;
            }

            return null;
        }
    }
}
