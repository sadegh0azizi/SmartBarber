using SmartBarber.Application.Common.Errors;
using SmartBarber.Application.Common.Results;
using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Bookings.CreateBooking
{
    public record CreateBookingResult(
     ApplicationResultStatus Status,
     Guid? BookingId,
     ApplicationError? Error);
}
