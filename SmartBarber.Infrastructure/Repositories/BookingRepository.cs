using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartBarber.Application.Abstraction.Repositories;
using SmartBarber.Domain.Entities;
using SmartBarber.Domain.ValueObjects;
using SmartBarber.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly SmartBarberDbContext _context;

        public BookingRepository(SmartBarberDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasConflictAsync(
    Guid providerId,
    DateOnly date,
    TimeRange timeRange)
        {
            var connection = _context.Database.GetDbConnection();

            await using var command = connection.CreateCommand();

            command.CommandText = """
                    SELECT COUNT(1)
                    FROM Bookings WITH (UPDLOCK, HOLDLOCK)
                    WHERE ProviderId = @ProviderId
                      AND Date = @Date
                      AND StartTime < @EndTime
                      AND EndTime > @StartTime
                    """;

            command.Parameters.Add(new SqlParameter("@ProviderId", providerId));
            command.Parameters.Add(
                new SqlParameter("@Date", date.ToDateTime(TimeOnly.MinValue)));
            command.Parameters.Add(
                new SqlParameter("@StartTime", timeRange.Start));
            command.Parameters.Add(
                new SqlParameter("@EndTime", timeRange.End));

            var transaction = _context.Database.CurrentTransaction;

            if (transaction != null)
                command.Transaction = transaction.GetDbTransaction();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result) > 0;
        }

        public async Task<bool> HasConflictAsyncold(
            Guid providerId,
            DateOnly date,
            TimeRange timeRange)
        {
            return await _context.Bookings.AnyAsync(x =>
                x.ProviderId == providerId &&
                x.Date == date &&
                x.TimeRange.Start < timeRange.End &&
                x.TimeRange.End > timeRange.Start);
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }
    }
}
