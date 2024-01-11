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
        }
        #endregion Private classes
    }
}
