using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Proxy
{
    /// <summary>
    /// Прокси для репозитория операций с имитацией кэширования.
    /// </summary>
    public class ProxyOperationRepository : IOperationRepository
    {
        private readonly IOperationRepository _innerRepository;
        private List<Operation> _cache;

        public ProxyOperationRepository(IOperationRepository innerRepository)
        {
            _innerRepository = innerRepository;
            _cache = new List<Operation>();
        }

        public async Task AddAsync(Operation operation)
        {
            await _innerRepository.AddAsync(operation);
            _cache.Add(operation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _innerRepository.DeleteAsync(id);
            var op = _cache.FirstOrDefault(o => o.Id == id);
            if (op != null)
                _cache.Remove(op);
        }

        public async Task<IEnumerable<Operation>> GetAllAsync()
        {
            if (_cache.Any())
                return await Task.FromResult(_cache);

            _cache = (await _innerRepository.GetAllAsync()).ToList();
            return _cache;
        }

        public async Task<Operation> GetByIdAsync(Guid id)
        {
            var op = _cache.FirstOrDefault(o => o.Id == id);
            if (op != null)
                return await Task.FromResult(op);

            op = await _innerRepository.GetByIdAsync(id);
            if (op != null)
                _cache.Add(op);
            return op;
        }

        public async Task UpdateAsync(Operation operation)
        {
            await _innerRepository.UpdateAsync(operation);
            var index = _cache.FindIndex(o => o.Id == operation.Id);
            if (index >= 0)
                _cache[index] = operation;
        }
    }
}
