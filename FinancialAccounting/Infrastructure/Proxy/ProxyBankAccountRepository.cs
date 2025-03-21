using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Proxy
{
    /// <summary>
    /// Прокси для репозитория банковских счетов.
    /// Имитирует кэширование: если данные уже получены, возвращает их из локального списка.
    /// </summary>
    public class ProxyBankAccountRepository : IBankAccountRepository
    {
        private readonly IBankAccountRepository _innerRepository;
        private List<BankAccount> _cache;

        public ProxyBankAccountRepository(IBankAccountRepository innerRepository)
        {
            _innerRepository = innerRepository;
            _cache = new List<BankAccount>();
        }

        public async Task AddAsync(BankAccount account)
        {
            await _innerRepository.AddAsync(account);
            _cache.Add(account);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _innerRepository.DeleteAsync(id);
            var account = _cache.FirstOrDefault(a => a.Id == id);
            if (account != null)
                _cache.Remove(account);
        }

        public async Task<IEnumerable<BankAccount>> GetAllAsync()
        {
            if (_cache.Any())
                return await Task.FromResult(_cache);

            _cache = (await _innerRepository.GetAllAsync()).ToList();
            return _cache;
        }

        public async Task<BankAccount> GetByIdAsync(Guid id)
        {
            var account = _cache.FirstOrDefault(a => a.Id == id);
            if (account != null)
                return await Task.FromResult(account);

            account = await _innerRepository.GetByIdAsync(id);
            if (account != null)
                _cache.Add(account);
            return account;
        }

        public async Task UpdateAsync(BankAccount account)
        {
            await _innerRepository.UpdateAsync(account);
            var index = _cache.FindIndex(a => a.Id == account.Id);
            if (index >= 0)
                _cache[index] = account;
        }
    }
}
