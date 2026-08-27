using SmartBarber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Domain.Tests
{
    public  class CustomerTests
    {
        [Fact]
        public void Should_throw_when_phone_number_has_less_than_11_digits()
        {
            Assert.Throws<ArgumentException>(() =>
                new Customer(
                    Guid.NewGuid(),
                    "Mojtaba",
                    "0912345678"));
        }

        [Fact]
        public void Should_throw_when_phone_number_startWithNone09()
        {
            Assert.Throws<ArgumentException>(() =>
                new Customer(
                    Guid.NewGuid(),
                    "Mojtaba",
                    "+989198312509"));
        }
    }
}
