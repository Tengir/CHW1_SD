using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Proxy
{
    /// <summary>
    /// Прокси для репозитория категорий с имитацией кэширования.
    /// </summary>
    public class ProxyCategoryRepository : ICategoryRepository
    {
        private readonly ICategoryRepository _innerRepository;
        private List<Category> _cache;

        public ProxyCategoryRepository(ICategoryRepository innerRepository)
        {
            _innerRepository = innerRepository;
            _cache = new List<Category>();
        }

        public async Task AddAsync(Category category)
        {
            await _innerRepository.AddAsync(category);
            _cache.Add(category);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _innerRepository.DeleteAsync(id);
            var category = _cache.FirstOrDefault(c => c.Id == id);
            if (category != null)
                _cache.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            if (_cache.Any())
                return await Task.FromResult(_cache);

            _cache = (await _innerRepository.GetAllAsync()).ToList();
            return _cache;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = _cache.FirstOrDefault(c => c.Id == id);
            if (category != null)
                return await Task.FromResult(category);

            category = await _innerRepository.GetByIdAsync(id);
            if (category != null)
                _cache.Add(category);
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            await _innerRepository.UpdateAsync(category);
            var index = _cache.FindIndex(c => c.Id == category.Id);
            if (index >= 0)
                _cache[index] = category;
        }
    }
}
