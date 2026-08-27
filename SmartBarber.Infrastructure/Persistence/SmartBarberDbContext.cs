using Microsoft.EntityFrameworkCore;
using SmartBarber.Domain.Entities;

namespace SmartBarber.Infrastructure.Persistence
{
    public class SmartBarberDbContext : DbContext
    {
        public SmartBarberDbContext(
            DbContextOptions<SmartBarberDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(SmartBarberDbContext).Assembly);
        }

        public DbSet<Booking> Bookings { get; set; }

    }
}