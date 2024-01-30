using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves the specified number of characters from the beginning of the string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="numChars">The number of characters to retrieve.</param>
        /// <returns>A string containing the specified number of characters from the beginning of the input string.</returns>
        public static string GetFirstChars(this string input, int numChars)
        {
            return input.Substring(0, Math.Min(input.Length, numChars));
        }
    }
}
