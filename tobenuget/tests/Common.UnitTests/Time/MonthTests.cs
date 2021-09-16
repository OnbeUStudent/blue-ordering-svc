using DiiCommon.Time;
using FluentAssertions;
using Xunit;

namespace DiiCommon.Tests.Time
{
    public class MonthTests
    {
        [Theory]
        [InlineData(2020, 1, 202001)]
        [InlineData(2020, 2, 202002)]
        [InlineData(2020, 11, 202011)]
        [InlineData(2020, 12, 202012)]
        [InlineData(2020, 13, 202101)]
        [InlineData(2020, 24, 202112)]
        [InlineData(2020, 25, 202201)]
        public void WhenUsingYearMonthCtor_MonthId_ReturnsAsExpected(int year, int month, int predicted)
        {
            new Month(year, month).MonthId
                .Should().Be(predicted);
        }

        [Theory]
        [InlineData(202001)]
        [InlineData(202002)]
        [InlineData(202011)]
        [InlineData(202012)]
        [InlineData(202101)]
        [InlineData(202112)]
        [InlineData(202201)]
        public void WhenUsingMonthIdCtor_MonthId_ReturnsAsExpected(int monthId)
        {
            new Month(monthId).MonthId
                .Should().Be(monthId);
        }
    }
}
