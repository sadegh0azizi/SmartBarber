using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; }
        public string Name { get; }
        public TimeSpan Duration { get; }

        public Service(Guid id,string name,TimeSpan duration,decimal displayPrice)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (duration <= TimeSpan.Zero)
                throw new ArgumentException("Duration must be greater than zero.");

            if (displayPrice < 0)
                throw new ArgumentException("Display price cannot be negative.");

            Id = id;
            Name = name;
            Duration = duration;            
        }
    }
}
