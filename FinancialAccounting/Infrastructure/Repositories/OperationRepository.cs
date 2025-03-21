using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory реализация репозитория для операций.
    /// </summary>
    public class OperationRepository : IOperationRepository
    {
        private readonly List<Operation> _operations = new List<Operation>();

        public async Task AddAsync(Operation operation)
        {
            _operations.Add(operation);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var op = _operations.FirstOrDefault(o => o.Id == id);
            if (op != null)
                _operations.Remove(op);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Operation>> GetAllAsync()
        {
            return await Task.FromResult(_operations);
        }

        public async Task<Operation> GetByIdAsync(Guid id)
        {
            var op = _operations.FirstOrDefault(o => o.Id == id);
            return await Task.FromResult(op);
        }

        public async Task UpdateAsync(Operation operation)
        {
            // Обновление объекта происходит автоматически.
            await Task.CompletedTask;
        }
    }
}