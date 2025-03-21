using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для обновления существующего банковского счёта.
    /// </summary>
    public class UpdateAccountCommand
    {
        public Guid AccountId { get; set; }
        public string NewName { get; set; }
    }
}