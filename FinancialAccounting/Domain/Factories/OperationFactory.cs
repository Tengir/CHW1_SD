using System;
using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Factories
{
    /// <summary>
    /// Фабрика для создания объектов Operation.
    /// </summary>
    public static class OperationFactory
    {
        /// <summary>
        /// Создаёт новую операцию.
        /// </summary>
        /// <param name="bankAccountId">Идентификатор счёта.</param>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="description">Описание (опционально).</param>
        /// <returns>Новый объект Operation.</returns>
        public static Operation Create(Guid bankAccountId, Guid categoryId, decimal amount, DateTime date, string description = null)
        {
            return new Operation(bankAccountId, categoryId, amount, date, description);
        }
    }
}