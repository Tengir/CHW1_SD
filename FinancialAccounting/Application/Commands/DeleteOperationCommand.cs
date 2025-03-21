using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для удаления операции.
    /// </summary>
    public class DeleteOperationCommand
    {
        public Guid OperationId { get; set; }
    }
}