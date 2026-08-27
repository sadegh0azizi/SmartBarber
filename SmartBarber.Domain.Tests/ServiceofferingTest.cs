using SmartBarber.Domain.Entities;
using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Tests
{   
    public class ServiceofferingTest
    {
        [Fact]
        public void Should_throw_when_service_id_is_empty()
        {
            Guid prividerid = Guid.NewGuid();

            Assert.Throws<ArgumentException>(() => new ServiceOffering(Guid.Empty, prividerid, 200, true));
        }


        [Fact]
        public void Should_add_availability()
        {
            ServiceOffering serviceOffering = new ServiceOffering(Guid.NewGuid(), Guid.NewGuid(), 200, true);
            Availability availability = new Availability(DayOfWeek.Friday, new TimeRange(TimeSpan.FromHours(6), TimeSpan.FromHours(18)), true);
            serviceOffering.AddAvailability(availability);


            //assert
            Assert.Single(serviceOffering.Availability);
            Assert.Contains(availability, serviceOffering.Availability);
        }

        [Fact]
        public void Should_throw_when_availability_is_null()
        {
            // Arrange
            var offering = new ServiceOffering(
                Guid.NewGuid(),
                Guid.NewGuid(),
                500000,
                true);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                offering.AddAvailability(null));
        }
    }
}
