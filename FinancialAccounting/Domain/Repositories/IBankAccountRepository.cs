using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Repositories
{
    /// <summary>
    /// Интерфейс для репозитория банковских счетов.
    /// Определяет основные методы доступа к данным.
    /// </summary>
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetByIdAsync(Guid id);
        Task<IEnumerable<BankAccount>> GetAllAsync();
        Task AddAsync(BankAccount account);
        Task UpdateAsync(BankAccount account);
        Task DeleteAsync(Guid id);
    }
}