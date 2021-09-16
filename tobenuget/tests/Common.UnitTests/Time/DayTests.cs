using DiiCommon.Time;
using FluentAssertions;
using Xunit;

namespace DiiCommon.Tests.Time
{
    public class DayTests
    {
        [Theory]
        [InlineData(2020, 1, 7, 20200107)]
        [InlineData(2020, 2, 7, 20200207)]
        [InlineData(2020, 11, 7, 20201107)]
        [InlineData(2020, 12, 7, 20201207)]
        [InlineData(2020, 02, 28, 20200228)]
        [InlineData(2020, 02, 29, 20200229)]
        public void WhenUsingYearDayIdCtor_DayId_ReturnsAsExpected(int year, int month, int day, int predicted)
        {
            new Day(year, month, day).DayId
                .Should().Be(predicted);
        }

        [Theory]
        [InlineData(20200107)]
        [InlineData(20200207)]
        [InlineData(20201107)]
        [InlineData(20201207)]
        [InlineData(20210107)]
        [InlineData(20211207)]
        [InlineData(20220107)]
        public void WhenUsingDayIdCtor_DayId_ReturnsAsExpected(int dayId)
        {
            new Day(dayId).DayId
                .Should().Be(dayId);
        }
    }
}
