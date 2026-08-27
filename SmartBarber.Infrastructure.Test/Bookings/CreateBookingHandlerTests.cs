using SmartBarber.Application.Abstraction;
using SmartBarber.Application.Abstraction.Repositories;
using SmartBarber.Application.Bookings.CreateBooking;
using SmartBarber.Application.Common.Results;
using SmartBarber.Domain.ValueObjects;
using SmartBarber.Infrastructure.Persistence;
using SmartBarber.Infrastructure.Repositories;
using Xunit;

namespace SmartBarber.Infrastructure.Test.Bookings
{
    public class CreateBookingHandlerTests
    {
        [Fact]
        public async Task CreateBooking_Should_Save_Booking()
        {
            await using var context = TestDatabase.CreateContext();

            IBookingRepository repository =
                new BookingRepository(context);

            IUnitOfWork unitOfWork =
                new UnitOfWork(context);

            var handler = new CreateBookingHandler(
                repository,
                unitOfWork);

            var command = new CreateBookingCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateOnly(2026, 8, 28),
                new TimeRange(
                TimeSpan.FromHours(10),
                TimeSpan.FromHours(11)),
                            200_000);

            var result = await handler.HandleAsync(command);

            Assert.Equal(
                ApplicationResultStatus.Success,
                result.Status);

            Assert.NotEqual(
                Guid.Empty,
                result.BookingId);


            await using var verifyContext = TestDatabase.CreateContext();

            var booking = await verifyContext.Bookings
                .FindAsync(result.BookingId);

            Assert.NotNull(booking);
            Assert.Equal(command.CustomerId, booking.CustomerId);
            Assert.Equal(command.ProviderId, booking.ProviderId);
            Assert.Equal(command.Date, booking.Date);
            Assert.Equal(command.DepositAmount, booking.DepositAmount);
        }
    }
}