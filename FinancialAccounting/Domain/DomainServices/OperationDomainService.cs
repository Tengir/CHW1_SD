using System;
using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.DomainServices
{
    /// <summary>
    /// Реализация доменного сервиса для операций.
    /// Проверяет корректность операции и применяет её к банковскому счёту.
    /// </summary>
    public class OperationDomainService : IOperationDomainService
    {
        public async Task<bool> ValidateAndApplyOperationAsync(BankAccount account, Operation operation)
        {
            // Проверка базовой валидности операции
            if (!operation.Validate())
                return false;

            // Если операция расход (Amount отрицательная), проверяем достаточность средств
            if (operation.Amount < 0 && Math.Abs(operation.Amount) > account.Balance)
                return false;

            // Применяем операцию (увеличиваем или уменьшаем баланс)
            operation.Apply(account);

            // Имитация асинхронного завершения (например, для транзакционной логики)
            await Task.CompletedTask;
            return true;
        }
    }
}