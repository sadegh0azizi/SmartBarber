using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Entities
{
    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; }
        public string PhoneNumber { get; }

        public Customer(
            Guid id,
            string name,
            string phoneNumber)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");

            if (phoneNumber.Length != 11)
                throw new ArgumentException("Phone number must contain exactly 11 digits.");

            if (!phoneNumber.StartsWith("09"))
                throw new ArgumentException("Phone number must start with 09.");

            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
