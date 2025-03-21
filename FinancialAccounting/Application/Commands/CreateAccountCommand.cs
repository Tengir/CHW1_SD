namespace FinancialAccounting.Application.Commands
{
    /// <summary>
    /// Команда для создания нового банковского счёта.
    /// Содержит данные, вводимые пользователем.
    /// </summary>
    public class CreateAccountCommand
    {
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
    }
}