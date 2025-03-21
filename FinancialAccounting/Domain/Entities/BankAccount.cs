using System;

namespace FinancialAccounting.Domain.Entities
{
    /// <summary>
    /// Представляет банковский счёт.
    /// Содержит название и текущий баланс.
    /// </summary>
    public class BankAccount : BaseEntity
    {
        /// <summary>
        /// Название банковского счёта.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Текущий баланс счёта.
        /// </summary>
        public decimal Balance { get; private set; }

        /// <summary>
        /// Конструктор для создания нового банковского счёта.
        /// </summary>
        /// <param name="name">Название счёта.</param>
        /// <param name="initialBalance">Начальный баланс.</param>
        public BankAccount(string name, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название не может быть пустым.", nameof(name));

            Name = name;
            Balance = initialBalance;
        }

        /// <summary>
        /// Увеличивает баланс счёта.
        /// </summary>
        /// <param name="amount">Сумма пополнения.</param>
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма пополнения должна быть положительной.", nameof(amount));

            Balance += amount;
        }

        /// <summary>
        /// Уменьшает баланс счёта.
        /// </summary>
        /// <param name="amount">Сумма списания.</param>
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма списания должна быть положительной.", nameof(amount));

            if (amount > Balance)
                throw new InvalidOperationException("Недостаточно средств на счёте.");

            Balance -= amount;
        }

        /// <summary>
        /// Обновляет название счёта.
        /// </summary>
        /// <param name="newName">Новое название.</param>
        public void UpdateDetails(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Новое название не может быть пустым.", nameof(newName));

            Name = newName;
        }
    }
}
