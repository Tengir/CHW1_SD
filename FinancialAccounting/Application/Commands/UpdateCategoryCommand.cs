using System;

namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для обновления категории.
    /// </summary>
    public class UpdateCategoryCommand
    {
        public Guid CategoryId { get; set; }
        public string NewName { get; set; }
    }
}