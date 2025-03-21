using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для обновления операции.
    /// </summary>
    public class UpdateOperationCommand
    {
        public Guid OperationId { get; set; }
        // Пример: новые значения для суммы и описания; можно расширить при необходимости
        public decimal NewAmount { get; set; }
        public string NewDescription { get; set; }
    }
}