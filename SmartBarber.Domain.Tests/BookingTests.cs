using SmartBarber.Domain.Entities;
using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Tests
{
    public class BookingTests
    {
        [Fact]
        public void DateEqualButTimeHasConfilict_shourReurtnTrue()
        {
            Booking booking1 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateOnly(2026, 8, 21), new TimeRange(new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0)),5000);
            Booking booking2 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateOnly(2026, 8, 21), new TimeRange(new TimeSpan(10, 30, 0), new TimeSpan(11, 30, 0)), 5000);

            
            bool reult = booking1.ConflictWith(booking2);           

            Assert.True(reult);

        }

        [Fact]
        public void DateIsNotEqualButTimeIsEqual_shourReurtnFalse()
        {
            Booking booking1 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateOnly(2026, 8, 21), new TimeRange(new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0)), 5000);
            Booking booking2 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateOnly(2026, 8, 20), new TimeRange(new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0)),5000);


            bool result = booking1.ConflictWith(booking2);

            Assert.False(result);
        }

        [Fact]
        public void SameDate_TimeDoesNotOverlap_ShouldNotConflict()
        {
            Booking booking1 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),new DateOnly(2026, 8, 21), new TimeRange(new TimeSpan(10, 0, 0), new TimeSpan(11, 0, 0)), 5000);
            Booking booking2 = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new DateOnly(2026, 8, 21), new TimeRange(new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0)),5000);


            bool result = booking1.ConflictWith(booking2);

            Assert.False(result);
        }

        [Fact]
        public void Should_not_conflict_when_providers_and_customers_are_different()
        {
            var booking1 = new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateOnly(2026, 8, 25),
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            var booking2 = new Booking(
                Guid.NewGuid(),
                booking1.ServiceId,
                Guid.NewGuid(),
                booking1.Date,
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            Assert.False(booking1.ConflictWith(booking2));
        }

        [Fact]
        public void Should_conflict_when_same_provider_has_overlapping_bookings()
        {
            var providerId = Guid.NewGuid();
            var serviceId = Guid.NewGuid();
            var date = new DateOnly(2026, 8, 25);

            var booking1 = new Booking(
                Guid.NewGuid(),
                serviceId,
                providerId,
                date,
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            var booking2 = new Booking(
                Guid.NewGuid(),
                serviceId,
                providerId,
                date,
                new TimeRange(
                    TimeSpan.FromHours(10.5),
                    TimeSpan.FromHours(11.5)),
                200000);

            Assert.True(booking1.ConflictWith(booking2));
        }

        [Fact]
        public void Should_conflict_when_customers_are_same()
        {
            var customerId = Guid.NewGuid();

            var booking1 = new Booking(
                customerId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateOnly(2026, 8, 25),
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            var booking2 = new Booking(
                customerId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                booking1.Date,
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            Assert.True(booking1.ConflictWith(booking2));
        }

        [Fact]
        public void Should_conflict_when_providers_are_same()
        {
            var providerId = Guid.NewGuid();

            var booking1 = new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                providerId,
                new DateOnly(2026, 8, 25),
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            var booking2 = new Booking(
                Guid.NewGuid(),
                Guid.NewGuid(),
                providerId,
                booking1.Date,
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200000);

            Assert.True(booking1.ConflictWith(booking2));
        }
    }
}
