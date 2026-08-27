using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBarber.Application.Abstraction
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
        Task ExecuteInTransactionAsync(Func<Task> action);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action); 
    }
}
