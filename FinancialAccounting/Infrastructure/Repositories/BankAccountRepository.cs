using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory реализация репозитория для банковских счетов.
    /// </summary>
    public class BankAccountRepository : IBankAccountRepository
    {
        // In-memory хранилище банковских счетов.
        private readonly List<BankAccount> _accounts = new List<BankAccount>();

        public async Task AddAsync(BankAccount account)
        {
            _accounts.Add(account);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            if (account != null)
                _accounts.Remove(account);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<BankAccount>> GetAllAsync()
        {
            return await Task.FromResult(_accounts);
        }

        public async Task<BankAccount> GetByIdAsync(Guid id)
        {
            var account = _accounts.FirstOrDefault(a => a.Id == id);
            return await Task.FromResult(account);
        }

        public async Task UpdateAsync(BankAccount account)
        {
            // В in-memory репозитории объект обновляется по ссылке.
            await Task.CompletedTask;
        }
    }
}