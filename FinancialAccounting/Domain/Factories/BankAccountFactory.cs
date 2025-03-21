using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Factories
{
    /// <summary>
    /// Фабрика для создания объектов BankAccount.
    /// Гарантирует соблюдение бизнес-правил при создании.
    /// </summary>
    public static class BankAccountFactory
    {
        /// <summary>
        /// Создаёт новый банковский счёт.
        /// </summary>
        /// <param name="name">Название счёта.</param>
        /// <param name="initialBalance">Начальный баланс.</param>
        /// <returns>Новый объект BankAccount.</returns>
        public static BankAccount Create(string name, decimal initialBalance)
        {
            return new BankAccount(name, initialBalance);
        }
    }
}