using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.ValueObjects
{
    public class TimeRange : IEquatable<TimeRange>
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }

        public TimeSpan Duration => End - Start;

        public TimeRange(TimeSpan start, TimeSpan end)
        {
            if (start >= end)
                throw new ArgumentException("start time must be before end time!");
            Start = start;
            End = end;
        }

        public bool Overlaps(TimeRange otherTime)
        {
            return Start < otherTime.End && otherTime.Start < End;
        }

        public bool Equals(TimeRange? other)
        {
            if (other is null)
                return false;
            return other.Start == Start && other.End == End;
        }

        public bool Equals(object? other)
        {
            return Equals(other as TimeRange);
        }

        public static bool operator ==(TimeRange? left, TimeRange? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TimeRange? left, TimeRange? right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start.GetHashCode(), End.GetHashCode());
        }
    }
}
