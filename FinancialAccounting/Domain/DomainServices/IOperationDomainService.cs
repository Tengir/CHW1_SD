using System.Threading.Tasks;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.DomainServices
{
    /// <summary>
    /// Интерфейс доменного сервиса для операций.
    /// Выполняет комплексную проверку и применение операции к банковскому счёту.
    /// </summary>
    public interface IOperationDomainService
    {
        /// <summary>
        /// Валидирует операцию и применяет её к банковскому счёту.
        /// </summary>
        /// <param name="account">Банковский счёт.</param>
        /// <param name="operation">Операция.</param>
        /// <returns>True, если операция успешно применена.</returns>
        Task<bool> ValidateAndApplyOperationAsync(BankAccount account, Operation operation);
    }
}