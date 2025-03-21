using System;

namespace FinancialAccounting.Application.DTOs
{
    /// <summary>
    /// DTO для категории.
    /// </summary>
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}