using System;
using System.Text;
using System.Text.RegularExpressions;

namespace C4PHub.Core.Utilities
{
    public static class StringUtility
    {
        public static string ConvertToBase64(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string GenerateIdFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            input = input.ToLower().Trim();
            if (input.EndsWith("/"))
                input = input.Remove(input.Count() - 1);
            return ConvertToBase64(input);
        }


        private const string AMPMPattern = @"\d{1,2}:\d{2} (AM|PM)";
        /// <summary>
        /// Extracts the AM/PM part from the input string.
        /// </summary>
        /// <remarks>
        /// The method extracts The AM/PM part from strings like the following:
        /// - "Call closes at 12:00 PM" --> "12:00 PM"
        /// - "something 01:00 AM something" --> "01:00 AM"
        /// - "10:30 PM something" --> "10:30 PM"
        /// </remarks>
        /// <param name="input">The input string.</param>
        /// <returns>The extracted AM/PM part if found, otherwise null.</returns>
        public static string ExtractAMPMPart(string input)
        {
            if (input == null)
                return null;

            Regex regex = new Regex(AMPMPattern);

            Match match = regex.Match(input);
            if (match.Success)
                return match.Value;
            else
                return null;
        }



        private const string UTCPattern = @"(UTC[+-]\d{2}:\d{2})";
        /// <summary>
        /// Extracts the UTC part from the input string.
        /// </summary>
        /// <remarks>
        /// The method extracts The AM/PM part from strings like the following:
        /// - "W. Europe Standard Time (UTC+01:00)" --> "UTC+01:00"
        /// - "W. Europe Standard Time (UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna" --> "UTC+01:00"
        /// - "(UTC+01:00) W. Europe Standard Time" --> "UTC+01:00"
        /// - "(UTC+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna" --> "UTC+01:00"
        /// </remarks>
        /// <param name="input">The input string.</param>
        /// <returns>The extracted UTC part if found, otherwise null.</returns>
        public static string ExtractUTCPart(string input)
        {
            if (input == null)
                return null;

            Regex regex = new Regex(UTCPattern);
            Match match = regex.Match(input);
            if (match.Success)
                return match.Value;
            else
                return null;
        }

    }
}
