using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Entities
{
    public class ServiceProvider
    {
        public Guid Id { get; }
        public string Name { get; }
        public int ExperienceYears { get; }

        public ServiceProvider(Guid id ,string name,int experienceYears)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty."); 

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (experienceYears < 0)
                throw new ArgumentException("Experience years cannot be negative.");

            Id = id;
            Name = name;
            ExperienceYears = experienceYears;
        }
    }
}
