using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Entities
{
    public class Booking
    {
        private Booking()
        {
        }
        public Guid Id { get; }
        public Guid CustomerId { get;}
        public Guid ServiceId { get; }
        public Guid ProviderId { get; }
        public DateOnly Date { get; }
        public TimeRange TimeRange { get; }
        public DateTime RegisteredAt { get; }
        public decimal DepositAmount { get; }

        public Booking(Guid customerId,Guid serviceId, Guid providerId,DateOnly date, TimeRange timeRange, decimal depositAmount)
        {
            if (depositAmount < 0)
                throw new ArgumentException("Deposit amount cannot be negative.");

            if (customerId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty.");

            if (serviceId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty.");

            if (providerId == Guid.Empty)
                throw new ArgumentException("CustomerId cannot be empty.");
            
            Id = Guid.NewGuid();
            Date = date;
            TimeRange = timeRange;
            DepositAmount = depositAmount;
            CustomerId = customerId;
            ServiceId = serviceId;
            ProviderId = providerId;
            RegisteredAt = DateTime.Now;
        }

        public bool ConflictWith(Booking other) 
        {
            if (Date != other.Date)
                return false;

            if (!TimeRange.Overlaps(other.TimeRange))
                return false;

            return ProviderId == other.ProviderId ||
                   CustomerId == other.CustomerId;
        }
    }
}
