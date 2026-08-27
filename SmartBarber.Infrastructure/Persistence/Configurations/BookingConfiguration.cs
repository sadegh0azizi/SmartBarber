using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBarber.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.ProviderId,
                x.Date
            });

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.ServiceId)
                .IsRequired();

            builder.Property(x => x.ProviderId)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.DepositAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.RegisteredAt)
                .IsRequired();

            builder.ComplexProperty(x => x.TimeRange, timeRange =>
            {
                timeRange.Property(x => x.Start)
                    .HasColumnName("StartTime");

                timeRange.Property(x => x.End)
                    .HasColumnName("EndTime");
            });
        }
    }
}
