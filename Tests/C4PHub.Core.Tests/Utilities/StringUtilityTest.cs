using C4PHub.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Tests.Utilities
{
    public class StringUtilityTest
    {
        #region ConvertToBase64
        [Fact]
        public void ConvertToBase64_ReturnsNull_WhenInputIsNull()
        {
            string input = null;

            var result = StringUtility.ConvertToBase64(input);

            Assert.Null(result);
        }

        [Fact]
        public void ConvertToBase64_ReturnsNull_WhenInputIsEmpty()
        {
            string input = string.Empty;

            var result = StringUtility.ConvertToBase64(input);

            Assert.Null(result);
        }

        [Fact]
        public void ConvertToBase64_ReturnsNull_WhenInputIsWhiteSpaces()
        {
            string input = " ";

            var result = StringUtility.ConvertToBase64(input);

            Assert.Null(result);
        }
        #endregion ConvertToBase64

        #region GenerateIdFromString
        [Fact]
        public void GenerateIdFromString_ReturnsNull_WhenInputIsNull()
        {
            string input = null;

            var result = StringUtility.GenerateIdFromString(input);

            Assert.Null(result);
        }

        [Fact]
        public void GenerateIdFromString_ReturnsNull_WhenInputIsEmpty()
        {
            string input = string.Empty;

            var result = StringUtility.GenerateIdFromString(input);

            Assert.Null(result);
        }

        [Fact]
        public void GenerateIdFromString_ReturnsNull_WhenInputIsWhiteSpaces()
        {
            string input = " ";

            var result = StringUtility.GenerateIdFromString(input);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetWebUrlToConsiderTheSame), MemberType = typeof(DataGenerator))]
        public void GenerateIdFromString_ShouldGenerateSameId_WhenUrlsAreTheSame(string url1, string url2)
        {
            var result1 = StringUtility.GenerateIdFromString(url1);
            var result2 = StringUtility.GenerateIdFromString(url2);

            Assert.Equal(result1, result2);
        }
        #endregion GenerateIdFromString

        #region ExtractAMPMPart
        [Fact]
        public void ExtractAMPMPart_ReturnsNull_WhenInputIsNull()
        {
            string input = null;

            var result = StringUtility.ExtractAMPMPart(input);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetStringsWithAMPM), MemberType = typeof(DataGenerator))]
        public void ExtractAMPMPart_ShouldExtractTheRightTime_WhenTheStringIsWellFormed(string fullString, string timeString)
        {
            var result = StringUtility.ExtractAMPMPart(fullString);

            Assert.Equal(result, timeString);
        }

        #endregion

        #region ExtractUTCPart
        [Fact]
        public void ExtractUTCPart_ReturnsNull_WhenInputIsNull()
        {
            string input = null;

            var result = StringUtility.ExtractUTCPart(input);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetStringsWithUTC), MemberType = typeof(DataGenerator))]
        public void ExtractUTCPart_ShouldExtractTheRightTime_WhenTheStringIsWellFormed(string fullString, string timeString)
        {
            var result = StringUtility.ExtractUTCPart(fullString);

            Assert.Equal(result, timeString);
        }
        #endregion ExtractUTCPart

        #region ConvertUtcStringToTimeSpan
        [Theory]
        [MemberData(nameof(DataGenerator.GetWellFormedUTCStrings), MemberType = typeof(DataGenerator))]
        public void ConvertUtcStringToTimeSpan_ShouldGenerateTheRightTimeSpan_WhenTheStringIsWellFormed(string utcString, TimeSpan expectedTimeSpan)
        {
            var result = StringUtility.ConvertUtcStringToTimeSpan(utcString);

            Assert.Equal(result, expectedTimeSpan);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetWrongFormedUTCStrings), MemberType = typeof(DataGenerator))]
        public void ConvertUtcStringToTimeSpan_ShouldThrowException_WhenTheStringIsWrongFormed(string utcString)
        {
            Assert.Throws<ArgumentException>(() => StringUtility.ConvertUtcStringToTimeSpan(utcString));
        }
        #endregion ConvertUtcStringToTimeSpan

        #region Private classes
        private class DataGenerator
        {
            public static IEnumerable<object[]> GetWebUrlToConsiderTheSame()
            {
                yield return new object[]
                {
                    "https://www.website.com/evento1",
                    "https://www.website.com/evento1/",
                };
                yield return new object[]
                {
                    "https://WWW.WEBSITE.COM/evento1",
                    "https://www.website.com/evento1",
                };
                yield return new object[]
                {
                    "https://WWW.WEBSITE.COM/evento1",
                    "https://www.website.com/evento1/",
                };
                yield return new object[]
                {
                    "https://www.website.com/evento1",
                    "https://www.website.com/evento1",
                };
            }

            public static IEnumerable<object[]> GetStringsWithAMPM()
            {
                yield return new object[] { "pippo", null };
                yield return new object[] { "12:00 PM", "12:00 PM" };
                yield return new object[] { "Call closes at 12:00 PM", "12:00 PM" };
                yield return new object[] { "The meeting is at 02:15 PM", "02:15 PM" };
                yield return new object[] { "Hello 05:30 AM", "05:30 AM" };
                yield return new object[] { "09:45 PM Good night", "09:45 PM" };
                yield return new object[] { "Lunch time 12:30 PM", "12:30 PM" };
                yield return new object[] { "07:10 AM Wake up", "07:10 AM" };
                yield return new object[] { "06:00 PM Dinner", "06:00 PM" };
                yield return new object[] { "Happy birthday 10:00 AM", "10:00 AM" };
                yield return new object[] { "04:20 PM Blaze it", "04:20 PM" };
                yield return new object[] { "08:05 AM Have a nice day", "08:05 AM" };
                yield return new object[] { "03:50 PM Coffee break", "03:50 PM" };
            }

            public static IEnumerable<object[]> GetStringsWithUTC()
            {
                yield return new object[] { "pippo", null };
                yield return new object[] { "UTC-01:00", "UTC-01:00" };
                yield return new object[] { "UTC+01:00 is the current timezone in Italy", "UTC+01:00" };
                yield return new object[] { "The server is located in UTC-05:00", "UTC-05:00" };
                yield return new object[] { "Nairobi UTC+03:00 ", "UTC+03:00" };
                yield return new object[] { "UTC+08:00 Beijing", "UTC+08:00" };
                yield return new object[] { "UTC-03:00 Buenos Aires", "UTC-03:00" };
                yield return new object[] { "UTC+10:00 Sydney", "UTC+10:00" };
                yield return new object[] { "UTC-08:00 Los Angeles", "UTC-08:00" };
                yield return new object[] { "UTC+05:30 New Delhi", "UTC+05:30" };
                yield return new object[] { "Mexico City (UTC-06:00) ", "UTC-06:00" };
                yield return new object[] { "  UTC+02:00 Cairo  ", "UTC+02:00" };
            }

            public static IEnumerable<object[]> GetWellFormedUTCStrings()
            {
                yield return new object[] { "utc+00:00", TimeSpan.FromHours(0) };
                yield return new object[] { "utc+01:00", TimeSpan.FromHours(1) };
                yield return new object[] { "utc+02:00", TimeSpan.FromHours(2) };
                yield return new object[] { "utc+03:00", TimeSpan.FromHours(3) };
                yield return new object[] { "utc+04:00", TimeSpan.FromHours(4) };
                yield return new object[] { "utc+05:00", TimeSpan.FromHours(5) };
                yield return new object[] { "Utc+06:00", TimeSpan.FromHours(6) };
                yield return new object[] { "utc+07:00", TimeSpan.FromHours(7) };
                yield return new object[] { "UTC+08:00", TimeSpan.FromHours(8) };
                yield return new object[] { "utc+09:00", TimeSpan.FromHours(9) };
                yield return new object[] { "utc+10:00", TimeSpan.FromHours(10) };
                yield return new object[] { "utc-01:00", TimeSpan.FromHours(-1) };
                yield return new object[] { "UTC-02:00", TimeSpan.FromHours(-2) };
                yield return new object[] { "utc-03:00", TimeSpan.FromHours(-3) };
                yield return new object[] { "utc-04:00", TimeSpan.FromHours(-4) };
                yield return new object[] { "utc-05:00", TimeSpan.FromHours(-5) };
                yield return new object[] { "utC-06:00", TimeSpan.FromHours(-6) };
                yield return new object[] { "utc-07:00", TimeSpan.FromHours(-7) };
                yield return new object[] { "Utc-08:00", TimeSpan.FromHours(-8) };
                yield return new object[] { "uTc-09:00", TimeSpan.FromHours(-9) };
                yield return new object[] { "utc-10:00", TimeSpan.FromHours(-10) };
            }

            public static IEnumerable<object[]> GetWrongFormedUTCStrings()
            {
                yield return new object[] { "ut+00:00" };
                yield return new object[] { "uTc00:00"};
                yield return new object[] { "" };
                yield return new object[] { "wrongformat" };
            }
        }
        #endregion Private classes
    }
}
