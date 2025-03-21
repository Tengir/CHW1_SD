using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Repositories
{
    /// <summary>
    /// Интерфейс для репозитория категорий.
    /// </summary>
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}