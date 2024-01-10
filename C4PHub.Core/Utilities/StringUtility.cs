using System;
using System.Text;

namespace C4PHub.Core.Utilities
{
    internal static class StringUtility
    {
        public static string ConvertToBase64(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string GenerateIdFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            input = input.ToLower().Trim();
            if (input.EndsWith("/"))
                input = input.Remove(input.Count() - 1);
            return ConvertToBase64(input);
        }

    }
}
