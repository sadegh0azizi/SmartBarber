using System;
using System.Collections.Generic;
using System.Text;
using SmartBarber.Domain.ValueObjects;

namespace SmartBarber.Domain.Tests
{
    public class TimeRangeTests
    {
        [Fact]

        public void Constructor_WhenStartIsAfterEnd_ShouldThrow()
        {
            var start = TimeSpan.FromHours(10);
            var end = TimeSpan.FromHours(8);


            Action act = () =>
            {
                new TimeRange(start, end);
            };

            Assert.Throws<ArgumentException>(act);
        }
    }
}
