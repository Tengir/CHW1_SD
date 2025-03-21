using System;

namespace FinancialAccounting.Application.DTOs
{
    /// <summary>
    /// DTO для операции.
    /// </summary>
    public class OperationDTO
    {
        public Guid Id { get; set; }
        public Guid BankAccountId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}