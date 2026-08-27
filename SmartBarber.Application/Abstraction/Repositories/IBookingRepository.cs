    using SmartBarber.Domain.Entities;
    using SmartBarber.Domain.ValueObjects;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace SmartBarber.Application.Abstraction.Repositories
    {
        public interface IBookingRepository
        {
            Task<bool> HasConflictAsync(Guid providerId, DateOnly date, TimeRange timeRange);
            Task AddAsync(Booking booking);
        }
    }
