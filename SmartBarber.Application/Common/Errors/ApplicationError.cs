using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Common.Errors
{
    public record ApplicationError(string Code)
    {
        public static readonly ApplicationError BookingConflict =
        new("BOOKING_CONFLICT");

        public static readonly ApplicationError InvalidService =
            new("INVALID_SERVICE");

        public static readonly ApplicationError ProviderUnavailable =
            new("PROVIDER_UNAVAILABLE");
    }
}
