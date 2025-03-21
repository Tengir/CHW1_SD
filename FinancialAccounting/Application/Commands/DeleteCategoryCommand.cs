using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для удаления категории.
    /// </summary>
    public class DeleteCategoryCommand
    {
        public Guid CategoryId { get; set; }
    }
}