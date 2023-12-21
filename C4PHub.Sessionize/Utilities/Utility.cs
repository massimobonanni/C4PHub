using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace C4PHub.Sessionize.Utilities
{
    internal static class Utility
    {
        public static string CleanInnerText(string text)
        {
            var escapingText = text.Replace("\n", "").Replace("\t", "").Replace("\r", "").Trim();
            return RemoveExtraSpaces(escapingText);
        }

        public static string RemoveExtraSpaces(string input)
        {
            return Regex.Replace(input, @"\s{2,}", " ");
        }
    }
}
