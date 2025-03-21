using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для создания новой операции (транзакции).
    /// </summary>
    public class CreateOperationCommand
    {
        public Guid BankAccountId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}