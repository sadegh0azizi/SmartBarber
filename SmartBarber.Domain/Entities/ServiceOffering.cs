using SmartBarber.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Entities
{
    public class ServiceOffering
    {
        public Guid ServiceId { get; set; }
        public Guid ProviderId { get; set; }
        public decimal Price{ get; set; }
        public bool IsActive { get; set; }
        private readonly List<Availability> _availabilities = new();
        public IReadOnlyCollection<Availability> Availability  => _availabilities.AsReadOnly();

        public ServiceOffering(Guid serviceId,Guid providerId,decimal price,bool isActive)
        {
            if (serviceId == Guid.Empty)
                throw new ArgumentException("ServiceId cannot be empty.");

            if (providerId == Guid.Empty)
                throw new ArgumentException("ProviderId cannot be empty.");

            if (price < 0)
                throw new ArgumentException("Price cannot be negative.");

            ServiceId = serviceId;
            ProviderId = providerId;
            Price = price;
            IsActive = isActive;
        }

        public void AddAvailability(Availability availability)
        {
            if (availability == null)
                throw new ArgumentNullException(nameof(availability));

            _availabilities.Add(availability);
        }
    }
}
