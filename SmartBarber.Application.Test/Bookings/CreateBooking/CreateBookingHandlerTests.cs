using Moq;
using Xunit;
using SmartBarber.Domain.Entities;
using SmartBarber.Application.Abstraction;
using SmartBarber.Application.Abstraction.Repositories;
using SmartBarber.Application.Bookings.CreateBooking;
using SmartBarber.Application.Common.Errors;
using SmartBarber.Application.Common.Results;
using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Test.Bookings.CreateBooking
{
    public class CreateBookingHandlerTests
    {
        [Fact]
        public async Task HandleAsync_WhenBookingHasConflict_ShouldReturnFailedResult()
        {
            // Arrange

            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            bookingRepositoryMock
                .Setup(x => x.HasConflictAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<TimeRange>()))
                .ReturnsAsync(true);

            var command = new CreateBookingCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateOnly.FromDateTime(DateTime.Today),
                  new TimeRange(
                         TimeSpan.FromHours(10),
                         TimeSpan.FromHours(11)),
                200_000);

            var handler = new CreateBookingHandler(
                bookingRepositoryMock.Object,
                unitOfWorkMock.Object);

            // Act

            var result = await handler.HandleAsync(command);

            // Assert

            Assert.Equal(
                ApplicationResultStatus.Failed,
                result.Status);

            Assert.Equal(
                ApplicationError.BookingConflict,
                result.Error);

            bookingRepositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Booking>()),
                Times.Never);

            unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Never);
        }

        [Fact]
        public async Task HandleAsync_WhenBookingIsValid_ShouldCreateBookingSuccessfully()
        {
            // Arrange

            var bookingRepositoryMock = new Mock<IBookingRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            bookingRepositoryMock
                .Setup(x => x.HasConflictAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<TimeRange>()))
                .ReturnsAsync(false);

            var command = new CreateBookingCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateOnly.FromDateTime(DateTime.Today),
                new TimeRange(
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(11)),
                200_000);

            var handler = new CreateBookingHandler(
                bookingRepositoryMock.Object,
                unitOfWorkMock.Object);

            // Act

            var result = await handler.HandleAsync(command);

            // Assert

            Assert.Equal(
                ApplicationResultStatus.Success,
                result.Status);

            Assert.NotNull(result.BookingId);

            Assert.Null(result.Error);

            bookingRepositoryMock.Verify(
                x => x.AddAsync(
                    It.Is<Booking>(b =>
                        b.CustomerId == command.CustomerId &&
                        b.ServiceId == command.ServiceId &&
                        b.ProviderId == command.ProviderId &&
                        b.Date == command.Date &&
                        b.TimeRange == command.TimeRange &&
                        b.DepositAmount == command.DepositAmount)),
                Times.Once);

            unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}
