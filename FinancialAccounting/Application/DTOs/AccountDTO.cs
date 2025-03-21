using System;

namespace FinancialAccounting.Application.DTOs
{
    /// <summary>
    /// DTO для банковского счёта.
    /// Используется для передачи данных между слоями приложения.
    /// </summary>
    public class AccountDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}