using DiiCommon.Time;
using FluentAssertions;
using System;
using Xunit;

namespace DiiCommon.Tests.Time
{
    public class MonthsStartingAtTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(DiiConstants.Theaters.BookingWindowSizeInMonths)]
        [InlineData(DiiConstants.Theaters.BookingWindowSizeInMonths + 1)]
        public void Months_ContainsExpectedCount(int count)
        {
            // Arrange and Act
            var months = new MonthsStartingAt(DateTime.Now, count);

            // Assert
            months.Months.Count.Should().Be(count);
        }

        [Theory]
        [InlineData("05/01/2008 8:30 AM +01:00", 1)]
        [InlineData("05/01/2008 8:30 AM +01:00", 25)]
        public void Months_ContainsExpectedValues(string specifiedDateAsString, int count)
        {
            // Arrange and Act
            DateTime specifiedDate = DateTime.Parse(specifiedDateAsString);
            var months = new MonthsStartingAt(specifiedDate, count);

            // Assert
            months[0].MonthNumber.Should().Be(specifiedDate.Month);
            months[0].YearNumber.Should().Be(specifiedDate.Year);
            for (int i = 0; i < count; i++)
            {
                int predictedYear = specifiedDate.Year;
                int predictedMonth = specifiedDate.Month + i;

                while (predictedMonth > 12)
                {
                    predictedYear += 1;
                    predictedMonth -= 12;
                }
                months[i].MonthNumber.Should().Be(predictedMonth);
                months[i].YearNumber.Should().Be(predictedYear);
            }
        }
    }
}
