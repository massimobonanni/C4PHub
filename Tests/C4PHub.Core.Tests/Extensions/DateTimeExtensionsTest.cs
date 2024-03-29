﻿namespace C4PHub.Core.Tests.Extensions
{
    public class DateTimeExtensionsTest
    {
        #region ToStandardFormatString
        [Theory]
        [MemberData(nameof(DataGenerator.GetDateTimeOffsetAndStandardStrings), MemberType = typeof(DataGenerator))]
        public void ToStandardFormatString_ReturnsCorrectFormat_WhenInputIsValid(DateTimeOffset sourceOffset, string expectedString)
        {
            var result = sourceOffset.ToStandardFormatString();

            Assert.Equal(expectedString, result);
        }
        #endregion ToStandardFormatString

        #region ToUniversalFormatString
        [Theory]
        [MemberData(nameof(DataGenerator.GetDateTimeOffsetAndUniversalStrings), MemberType = typeof(DataGenerator))]
        public void ToUniversalFormatString_ReturnsCorrectFormat_WhenInputIsValid(DateTimeOffset sourceOffset, string expectedString)
        {
            var result = sourceOffset.ToUniversalFormatString();

            Assert.Equal(expectedString, result);
        }
        #endregion ToUniversalFormatString

        #region Private classes
        private class DataGenerator
        {
            public static IEnumerable<object[]> GetDateTimeOffsetAndStandardStrings()
            {
                yield return new object[]    {
                    new DateTimeOffset(new DateTime(2022, 3, 4, 5, 6, 7), TimeSpan.FromHours(-8)), "2022-03-04T05:06:07-08:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 6, 12, 10, 30, 0), TimeSpan.FromHours(2)), "2022-06-12T10:30:00+02:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 9, 20, 15, 45, 0), TimeSpan.FromHours(5)), "2022-09-20T15:45:00+05:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 12, 31, 23, 59, 59), TimeSpan.FromHours(-5)), "2022-12-31T23:59:59-05:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 2, 14, 12, 0, 0), TimeSpan.FromHours(0)), "2023-02-14T12:00:00Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 5, 1, 8, 15, 30), TimeSpan.FromHours(-3)), "2023-05-01T08:15:30-03:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 8, 10, 18, 45, 0), TimeSpan.FromHours(7)), "2023-08-10T18:45:00+07:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 11, 22, 6, 30, 0), TimeSpan.FromHours(-6)), "2023-11-22T06:30:00-06:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2024, 4, 5, 16, 0, 0), TimeSpan.FromHours(4)), "2024-04-05T16:00:00+04:00"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2024, 7, 15, 9, 45, 0), TimeSpan.FromHours(-9)), "2024-07-15T09:45:00-09:00"
                };
            }

            public static IEnumerable<object[]> GetDateTimeOffsetAndUniversalStrings()
            {
                yield return new object[]    {
                    new DateTimeOffset(new DateTime(2022, 3, 4, 5, 6, 7), TimeSpan.FromHours(-8)), "20220304T130607Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 6, 12, 10, 30, 0), TimeSpan.FromHours(2)), "20220612T083000Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 9, 20, 15, 45, 0), TimeSpan.FromHours(5)), "20220920T104500Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2022, 12, 31, 23, 59, 59), TimeSpan.FromHours(-5)), "20230101T045959Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 2, 14, 12, 0, 0), TimeSpan.FromHours(0)), "20230214T120000Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 5, 1, 8, 15, 30), TimeSpan.FromHours(-3)), "20230501T111530Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 8, 10, 18, 45, 0), TimeSpan.FromHours(7)), "20230810T114500Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2023, 11, 22, 6, 30, 0), TimeSpan.FromHours(-6)), "20231122T123000Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2024, 4, 5, 16, 0, 0), TimeSpan.FromHours(4)), "20240405T120000Z"
                };
                yield return new object[]
                {
                    new DateTimeOffset(new DateTime(2024, 7, 15, 9, 45, 0), TimeSpan.FromHours(-9)), "20240715T184500Z"
                };
            }

        }
        #endregion Private classes
    }
}
