using DiiCommon.Time;
using FluentAssertions;
using System;
using Xunit;

namespace DiiCommon.Tests.Time
{
    public class DaysRemainingInMonthStartingAtTests
    {
        [Theory]
        // Thirty days hath September,

        [InlineData("09/01/2007", 30)]
        [InlineData("09/01/2008", 30)]
        [InlineData("09/30/2012", 01)]

        // April, June, and November,

        [InlineData("04/01/2007", 30)]
        [InlineData("04/01/2008", 30)]
        [InlineData("04/30/2012", 01)]

        [InlineData("06/01/2007", 30)]
        [InlineData("06/01/2008", 30)]
        [InlineData("06/30/2012", 01)]

        [InlineData("11/01/2007", 30)]
        [InlineData("11/01/2008", 30)]
        [InlineData("11/30/2012", 01)]

        // All the rest have thirty-one,

        [InlineData("01/01/2007", 31)]
        [InlineData("03/01/2007", 31)]
        [InlineData("05/01/2007", 31)]
        [InlineData("07/01/2007", 31)]
        [InlineData("08/01/2007", 31)]
        [InlineData("10/01/2007", 31)]
        [InlineData("12/01/2007", 31)]

        [InlineData("01/01/2008", 31)]
        [InlineData("03/01/2008", 31)]
        [InlineData("05/01/2008", 31)]
        [InlineData("07/01/2008", 31)]
        [InlineData("08/01/2008", 31)]
        [InlineData("10/01/2008", 31)]
        [InlineData("12/01/2008", 31)]

        [InlineData("01/31/2008", 01)]
        [InlineData("03/31/2012", 01)]
        [InlineData("05/31/2012", 01)]
        [InlineData("07/31/2012", 01)]
        [InlineData("08/31/2012", 01)]
        [InlineData("10/31/2012", 01)]
        [InlineData("12/31/2012", 01)]

        // But February's twenty-eight,

        [InlineData("02/01/2007", 28)] // Not a leap year

        // The leap year, which comes once in four,
        // Gives February one day more.

        [InlineData("02/01/2008", 29)] // Leap year
        [InlineData("02/29/2012", 01)] // Leap year

        // https://en.wikipedia.org/wiki/Thirty_Days_Hath_September

        public void Days_ContainsExpectedCount(string specifiedDateAsString, int predictedCount)
        {
            // Arrange and Act
            DateTime specifiedDate = DateTime.Parse(specifiedDateAsString);
            var months = new DaysRemainingInMonthStartingAt(specifiedDate);

            // Assert
            months.Days.Count.Should().Be(predictedCount,
                "because that's what the poem says");
            GetPredictedCount(specifiedDate).Should().Be(predictedCount,
                "because we're testing our test helper, GetPredictedCount(...), while we're here");
        }

        [Theory]
        // Thirty days hath September,

        [InlineData("09/01/2007")]
        [InlineData("09/01/2008")]
        [InlineData("09/30/2012")]

        // April, June, and November,

        [InlineData("04/01/2007")]
        [InlineData("04/01/2008")]
        [InlineData("04/30/2012")]

        [InlineData("06/01/2007")]
        [InlineData("06/01/2008")]
        [InlineData("06/30/2012")]

        [InlineData("11/01/2007")]
        [InlineData("11/01/2008")]
        [InlineData("11/30/2012")]

        // All the rest have thirty-one,

        [InlineData("01/01/2007")]
        [InlineData("03/01/2007")]
        [InlineData("05/01/2007")]
        [InlineData("07/01/2007")]
        [InlineData("08/01/2007")]
        [InlineData("10/01/2007")]
        [InlineData("12/01/2007")]

        [InlineData("01/01/2008")]
        [InlineData("03/01/2008")]
        [InlineData("05/01/2008")]
        [InlineData("07/01/2008")]
        [InlineData("08/01/2008")]
        [InlineData("10/01/2008")]
        [InlineData("12/01/2008")]

        [InlineData("01/31/2008")]
        [InlineData("03/31/2012")]
        [InlineData("05/31/2012")]
        [InlineData("07/31/2012")]
        [InlineData("08/31/2012")]
        [InlineData("10/31/2012")]
        [InlineData("12/31/2012")]

        // But February's twenty-eight,

        [InlineData("02/01/2007")] // Not a leap year

        // The leap year, which comes once in four,
        // Gives February one day more.

        [InlineData("02/01/2008")] // Leap year
        [InlineData("02/29/2012")] // Leap year

        // https://en.wikipedia.org/wiki/Thirty_Days_Hath_September

        public void Days_ContainsExpectedValues(string specifiedDateAsString)
        {
            // Arrange and Act
            DateTime specifiedDate = DateTime.Parse(specifiedDateAsString);
            var days = new DaysRemainingInMonthStartingAt(specifiedDate);

            // Assert
            days[0].MonthNumber.Should().Be(specifiedDate.Month);
            days[0].YearNumber.Should().Be(specifiedDate.Year);
            days[0].DayNumber.Should().Be(specifiedDate.Day);

            int predictedYear = specifiedDate.Year;
            int predictedMonth = specifiedDate.Month;
            int count = GetPredictedCount(specifiedDate);
            for (int i = 0; i < count; i++)
            {
                int predictedDay = specifiedDate.Date.Day + i;

                days[i].MonthNumber.Should().Be(predictedMonth);
                days[i].YearNumber.Should().Be(predictedYear);
                days[i].DayNumber.Should().Be(predictedDay);
            }
        }

        private int GetPredictedCount(DateTime date)
        {
            DateTime specifiedDateAsDateTime = new DateTime(date.Year, date.Month, date.Day);
            DateTime firstDayOfFollowingMonth;
            if (date.Month == 12)
            {
                firstDayOfFollowingMonth = new DateTime(date.Year + 1, 1, 1);
            }
            else 
            {
                firstDayOfFollowingMonth = new DateTime(date.Year, date.Month + 1, 1);
            }
            int count = (firstDayOfFollowingMonth - specifiedDateAsDateTime).Days;
            return count;
        }
    }
}
