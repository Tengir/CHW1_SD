using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для удаления банковского счёта.
    /// </summary>
    public class DeleteAccountCommand
    {
        public Guid AccountId { get; set; }
    }
}