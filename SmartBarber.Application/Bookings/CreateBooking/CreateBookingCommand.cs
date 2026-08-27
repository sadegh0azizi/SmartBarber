using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Bookings.CreateBooking
{
    public record CreateBookingCommand(
        Guid CustomerId,
        Guid ServiceId,
        Guid ProviderId,
        DateOnly Date,
        TimeRange TimeRange,
        decimal DepositAmount);
}
