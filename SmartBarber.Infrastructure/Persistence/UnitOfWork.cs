using Microsoft.EntityFrameworkCore;
using SmartBarber.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartBarberDbContext _dbContext;
        public UnitOfWork(SmartBarberDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action)
        {
            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = await action();

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> action)
        {
            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
