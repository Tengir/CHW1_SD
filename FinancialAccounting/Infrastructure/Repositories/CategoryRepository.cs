using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory реализация репозитория для категорий.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new List<Category>();

        public async Task AddAsync(Category category)
        {
            _categories.Add(category);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                _categories.Remove(category);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await Task.FromResult(_categories);
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(category);
        }

        public async Task UpdateAsync(Category category)
        {
            // Обновление объекта происходит автоматически.
            await Task.CompletedTask;
        }
    }
}