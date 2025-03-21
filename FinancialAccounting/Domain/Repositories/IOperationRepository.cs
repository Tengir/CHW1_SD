using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Repositories
{
    /// <summary>
    /// Интерфейс для репозитория операций.
    /// </summary>
    public interface IOperationRepository
    {
        Task<Operation> GetByIdAsync(Guid id);
        Task<IEnumerable<Operation>> GetAllAsync();
        Task AddAsync(Operation operation);
        Task UpdateAsync(Operation operation);
        Task DeleteAsync(Guid id);
    }
}