using Microsoft.EntityFrameworkCore;
using SmartBarber.Infrastructure.Persistence;

namespace SmartBarber.Infrastructure.Test
{
    public static class TestDatabase
    {
        public static SmartBarberDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<SmartBarberDbContext>()
                .UseSqlServer(
                    "Server=.;Database=SmartBarberDb;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;

            return new SmartBarberDbContext(options);
        }
    }
}