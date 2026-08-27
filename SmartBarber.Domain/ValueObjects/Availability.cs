using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.ValueObjects
{
    public class Availability
    {
        public DayOfWeek Day { get; }
        public TimeRange TimeRange { get; }
        public bool IsActive { get; }

        public Availability(
            DayOfWeek day,
            TimeRange timeRange,
            bool isActive)
        {
            Day = day;
            TimeRange = timeRange;
            IsActive = isActive;
        }
    }
}
