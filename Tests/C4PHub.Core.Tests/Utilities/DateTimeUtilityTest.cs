using C4PHub.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Tests.Utilities
{
    public class DateTimeUtilityTest
    {

        #region ParseStringToDateTimeOffset
        [Fact]
        public void ParseStringToDateTimeOffset_ReturnsNull_WhenInputIsNull()
        {
            string input = null;

            var result = DateTimeUtility.ParseStringToDateTimeOffset(input);

            Assert.Null(result);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetWellFormedDateTimeString), MemberType = typeof(DataGenerator))]
        public void ParseStringToDateTimeOffset_ReturnsDateTimeOffset_WhenInputIsWellFormed(string input, DateTimeOffset expected)
        {
            var result = DateTimeUtility.ParseStringToDateTimeOffset(input);

            Assert.Equal(expected, result);
        }
        #endregion

        #region Private classes
        private class DataGenerator
        {
            public static IEnumerable<object[]> GetWellFormedDateTimeString()
            {
                yield return new object[] { "1 Feb 2024", new DateTimeOffset(new DateTime(2024, 2, 1, 0, 0, 0)) };
                yield return new object[] { "15 Mar 2024 ", new DateTimeOffset(new DateTime(2024, 3, 15, 0, 0, 0)) };
                yield return new object[] { "30 Apr 2020", new DateTimeOffset(new DateTime(2020, 4, 30, 0, 0, 0)) };
                yield return new object[] { "14 May 2024", new DateTimeOffset(new DateTime(2024, 5, 14, 0, 0, 0)) };
                yield return new object[] { " 28 Jun 2024", new DateTimeOffset(new DateTime(2024, 6, 28, 0, 0, 0)) };
                yield return new object[] { "12 Jul 1970", new DateTimeOffset(new DateTime(1970, 7, 12, 0, 0, 0)) };
                yield return new object[] { "26 Aug 2024", new DateTimeOffset(new DateTime(2024, 8, 26, 0, 0, 0)) };
                yield return new object[] { "9 Sep 2024", new DateTimeOffset(new DateTime(2024, 9, 9, 0, 0, 0)) };
                yield return new object[] { "24 Oct 2000", new DateTimeOffset(new DateTime(2000, 10, 24, 0, 0, 0)) };
                yield return new object[] { " 7 Nov 1980", new DateTimeOffset(new DateTime(1980, 11, 7, 0, 0, 0)) };
                yield return new object[] { "1 Feb 2024 9:15 AM", new DateTimeOffset(new DateTime(2024, 2, 1, 9, 15, 0)) };
                yield return new object[] { "15 Mar 2024 3:30 PM", new DateTimeOffset(new DateTime(2024, 3, 15, 15, 30, 0)) };
                yield return new object[] { "30 Apr 2024 12:45 AM", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 45, 0)) };
                yield return new object[] { "14 May 2024 6:00 PM", new DateTimeOffset(new DateTime(2024, 5, 14, 18, 0, 0)) };
                yield return new object[] { "28 Jun 2024 4:15 AM", new DateTimeOffset(new DateTime(2024, 6, 28, 4, 15, 0)) };
                yield return new object[] { "12 Jul 2024 7:30 PM", new DateTimeOffset(new DateTime(2024, 7, 12, 19, 30, 0)) };
                yield return new object[] { "26 Aug 2024 1:45 AM", new DateTimeOffset(new DateTime(2024, 8, 26, 1, 45, 0)) };
                yield return new object[] { "9 Sep 2024 5:00 PM", new DateTimeOffset(new DateTime(2024, 9, 9, 17, 0, 0)) };
                yield return new object[] { "24 Oct 2024 2:15 AM ", new DateTimeOffset(new DateTime(2024, 10, 24, 2, 15, 0)) };
                yield return new object[] { "7 Nov 2024 8:30 PM", new DateTimeOffset(new DateTime(2024, 11, 7, 20, 30, 0)) };
                yield return new object[] { "1 Feb 2024 9:15 AM +01:00", new DateTimeOffset(new DateTime(2024, 2, 1, 9, 15, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { "15 Mar 2024 3:30 PM +01:00", new DateTimeOffset(new DateTime(2024, 3, 15, 15, 30, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { "30 Apr 2024 12:45 AM +02:00", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 45, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "14 May 2024 6:00 PM -03:00", new DateTimeOffset(new DateTime(2024, 5, 14, 18, 0, 0), TimeSpan.FromHours(-3)) };
                yield return new object[] { "28 Jun 2024 4:15 AM +02:00", new DateTimeOffset(new DateTime(2024, 6, 28, 4, 15, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "12 Jul 2024 7:30 PM +02:00", new DateTimeOffset(new DateTime(2024, 7, 12, 19, 30, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "26 Aug 2024 1:45 AM -08:00", new DateTimeOffset(new DateTime(2024, 8, 26, 1, 45, 0), TimeSpan.FromHours(-8)) };
                yield return new object[] { " 9 Sep 2024 5:00 PM +02:00", new DateTimeOffset(new DateTime(2024, 9, 9, 17, 0, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "24 Oct 2024 2:15 AM +01:00", new DateTimeOffset(new DateTime(2024, 10, 24, 2, 15, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { "7 Nov 2024 8:30 PM +00:00", new DateTimeOffset(new DateTime(2024, 11, 7, 20, 30, 0), TimeSpan.FromHours(0)) };
                yield return new object[] { "1 Feb 2024 9:15 AM UTC+01:00", new DateTimeOffset(new DateTime(2024, 2, 1, 9, 15, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { "15 Mar 2024 3:30 PM UTC+01:00", new DateTimeOffset(new DateTime(2024, 3, 15, 15, 30, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { " 30 Apr 2024 12:45 AM UTC-02:00", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 45, 0), TimeSpan.FromHours(-2)) };
                yield return new object[] { "14 May 2024 6:00 PM UTC+02:00", new DateTimeOffset(new DateTime(2024, 5, 14, 18, 0, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "28 Jun 2024 4:15 AM UTC+02:00 ", new DateTimeOffset(new DateTime(2024, 6, 28, 4, 15, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "12 Jul 2024 7:30 PM UTC+08:00", new DateTimeOffset(new DateTime(2024, 7, 12, 19, 30, 0), TimeSpan.FromHours(8)) };
                yield return new object[] { "26 Aug 2024 1:45 AM UTC+02:00", new DateTimeOffset(new DateTime(2024, 8, 26, 1, 45, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "9 Sep 2024 5:00 PM UTC+02:00", new DateTimeOffset(new DateTime(2024, 9, 9, 17, 0, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "24 Oct 2024 2:15 AM UTC-01:00 ", new DateTimeOffset(new DateTime(2024, 10, 24, 2, 15, 0), TimeSpan.FromHours(-1)) };
                yield return new object[] { "7 Nov 2024 8:30 PM UTC+01:00", new DateTimeOffset(new DateTime(2024, 11, 7, 20, 30, 0), TimeSpan.FromHours(1)) };
                //yield return new object[] { "Mon, 01 Feb 2024 09:15 UTC|+01:00", new DateTimeOffset(new DateTime(2024, 2, 1, 9, 15, 0), TimeSpan.FromHours(1)) };
                //yield return new object[] { "Fri, 15 Mar 2024 15:30 UTC|+01:00", new DateTimeOffset(new DateTime(2024, 3, 15, 15, 30, 0), TimeSpan.FromHours(1)) };
                //yield return new object[] { "Sat, 30 Apr 2024 00:45 UTC|+02:00", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 45, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Tue, 14 May 2024 18:00 UTC|+02:00", new DateTimeOffset(new DateTime(2024, 5, 14, 18, 0, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Sun, 28 Jun 2024 04:15 UTC|-02:00", new DateTimeOffset(new DateTime(2024, 6, 28, 4, 15, 0), TimeSpan.FromHours(-2)) };
                //yield return new object[] { "Fri, 12 Jul 2024 19:30 UTC|+02:00", new DateTimeOffset(new DateTime(2024, 7, 12, 19, 30, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Mon, 26 Aug 2024 01:45 UTC|+02:00", new DateTimeOffset(new DateTime(2024, 8, 26, 1, 45, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Wed, 09 Sep 2024 17:00 UTC|+08:00", new DateTimeOffset(new DateTime(2024, 9, 9, 17, 0, 0), TimeSpan.FromHours(8)) };
                //yield return new object[] { "Sat, 24 Oct 2024 02:15 UTC|-01:00", new DateTimeOffset(new DateTime(2024, 10, 24, 2, 15, 0), TimeSpan.FromHours(-1)) };
                //yield return new object[] { "Thu, 07 Nov 2024 20:30 UTC|+01:30", new DateTimeOffset(new DateTime(2024, 11, 7, 20, 30, 0), TimeSpan.FromHours(1.5)) };
                //yield return new object[] { "Mon, 01 Feb 2024 09:15 UTC+01:00", new DateTimeOffset(new DateTime(2024, 2, 1, 9, 15, 0), TimeSpan.FromHours(1)) };
                //yield return new object[] { "Fri, 15 Mar 2024 15:30 UTC+01:00", new DateTimeOffset(new DateTime(2024, 3, 15, 15, 30, 0), TimeSpan.FromHours(1)) };
                //yield return new object[] { "Sat, 30 Apr 2024 00:45 UTC+02:00", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 45, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Tue, 14 May 2024 18:00 UTC+02:00", new DateTimeOffset(new DateTime(2024, 5, 14, 18, 0, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Sun, 28 Jun 2024 04:15 UTC-02:00", new DateTimeOffset(new DateTime(2024, 6, 28, 4, 15, 0), TimeSpan.FromHours(-2)) };
                //yield return new object[] { "Fri, 12 Jul 2024 19:30 UTC+02:00", new DateTimeOffset(new DateTime(2024, 7, 12, 19, 30, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Mon, 26 Aug 2024 01:45 UTC+02:00", new DateTimeOffset(new DateTime(2024, 8, 26, 1, 45, 0), TimeSpan.FromHours(2)) };
                //yield return new object[] { "Wed, 09 Sep 2024 17:00 UTC+08:00", new DateTimeOffset(new DateTime(2024, 9, 9, 17, 0, 0), TimeSpan.FromHours(8)) };
                //yield return new object[] { "Sat, 24 Oct 2024 02:15 UTC-01:00", new DateTimeOffset(new DateTime(2024, 10, 24, 2, 15, 0), TimeSpan.FromHours(-1)) };
                //yield return new object[] { "Thu, 07 Nov 2024 20:30 UTC+01:30", new DateTimeOffset(new DateTime(2024, 11, 7, 20, 30, 0), TimeSpan.FromHours(1.5)) };
                yield return new object[] { "01/02/2024", new DateTimeOffset(new DateTime(2024, 2, 1, 0, 0, 0)) };
                yield return new object[] { "15/03/2024", new DateTimeOffset(new DateTime(2024, 3, 15, 0, 0, 0)) };
                yield return new object[] { "30/04/2024", new DateTimeOffset(new DateTime(2024, 4, 30, 0, 0, 0)) };
                yield return new object[] { "14/05/2024", new DateTimeOffset(new DateTime(2024, 5, 14, 0, 0, 0)) };
                yield return new object[] { "28/06/2024", new DateTimeOffset(new DateTime(2024, 6, 28, 0, 0, 0)) };
                yield return new object[] { "12/07/2024", new DateTimeOffset(new DateTime(2024, 7, 12, 0, 0, 0)) };
                yield return new object[] { "26/08/2024", new DateTimeOffset(new DateTime(2024, 8, 26, 0, 0, 0)) };
                yield return new object[] { "09/09/2024", new DateTimeOffset(new DateTime(2024, 9, 9, 0, 0, 0)) };
                yield return new object[] { "24/10/2024", new DateTimeOffset(new DateTime(2024, 10, 24, 0, 0, 0)) };
                yield return new object[] { "07/11/2024", new DateTimeOffset(new DateTime(2024, 11, 7, 0, 0, 0)) };
                yield return new object[] { "01/02/2024 12:34 UTC+01:00", new DateTimeOffset(new DateTime(2024, 2, 1, 12, 34, 0), TimeSpan.FromHours(1)) };
                yield return new object[] { "15/03/2024 23:45 UTC-05:00", new DateTimeOffset(new DateTime(2024, 3, 15, 23, 45, 0), TimeSpan.FromHours(-5)) };
                yield return new object[] { "30/04/2024 08:09 UTC+10:00", new DateTimeOffset(new DateTime(2024, 4, 30, 8, 9, 0), TimeSpan.FromHours(10)) };
                yield return new object[] { "14/05/2024 16:17 UTC-08:00", new DateTimeOffset(new DateTime(2024, 5, 14, 16, 17, 0), TimeSpan.FromHours(-8)) };
                yield return new object[] { "29/06/2024 19:28 UTC+02:00", new DateTimeOffset(new DateTime(2024, 6, 29, 19, 28, 0), TimeSpan.FromHours(2)) };
                yield return new object[] { "13/07/2024 04:36 UTC+05:30", new DateTimeOffset(new DateTime(2024, 7, 13, 4, 36, 0), TimeSpan.FromHours(5.5)) };
                yield return new object[] { "27/08/2024 10:44 UTC-03:00", new DateTimeOffset(new DateTime(2024, 8, 27, 10, 44, 0), TimeSpan.FromHours(-3)) };
                yield return new object[] { "11/09/2024 13:55 UTC+00:00", new DateTimeOffset(new DateTime(2024, 9, 11, 13, 55, 0), TimeSpan.FromHours(0)) };
                yield return new object[] { "26/10/2024 18:03 UTC+08:00", new DateTimeOffset(new DateTime(2024, 10, 26, 18, 3, 0), TimeSpan.FromHours(8)) };
                yield return new object[] { "10/11/2024 21:12 UTC-06:00", new DateTimeOffset(new DateTime(2024, 11, 10, 21, 12, 0), TimeSpan.FromHours(-6)) };


            }

        }
        #endregion Private classes
    }
}
