using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.StorageAccount.Utilities
{
    internal static class StringUtility
    {
        public static string RemoveControlChars(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c >= 0x20)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        private static IEnumerable<char> disallowedChars = new List<char>()
        { '/','\\','#','?','\t','\n','\r' };

        public static string RemoveDisallowedChars(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (!disallowedChars.Contains(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string ConvertToBase64(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

    }
}
