using System;

namespace FinancialAccounting.Domain.Entities
{
    /// <summary>
    /// Представляет операцию (транзакцию) для банковского счёта.
    /// Если сумма положительная – это доход, если отрицательная – расход.
    /// </summary>
    public class Operation : BaseEntity
    {
        /// <summary>
        /// Идентификатор банковского счёта, к которому относится операция.
        /// </summary>
        public Guid BankAccountId { get; private set; }

        /// <summary>
        /// Идентификатор категории операции.
        /// </summary>
        public Guid CategoryId { get; private set; }

        /// <summary>
        /// Сумма операции. Положительная – доход, отрицательная – расход.
        /// </summary>
        public decimal Amount { get; private set; }

        /// <summary>
        /// Дата проведения операции.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Описание операции (необязательно).
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Конструктор создания операции.
        /// </summary>
        /// <param name="bankAccountId">Идентификатор счёта.</param>
        /// <param name="categoryId">Идентификатор категории.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата операции.</param>
        /// <param name="description">Описание (опционально).</param>
        public Operation(Guid bankAccountId, Guid categoryId, decimal amount, DateTime date, string description = null)
        {
            if (bankAccountId == Guid.Empty)
                throw new ArgumentException("Идентификатор банковского счёта не может быть пустым.", nameof(bankAccountId));
            if (categoryId == Guid.Empty)
                throw new ArgumentException("Идентификатор категории не может быть пустым.", nameof(categoryId));
            if (amount == 0)
                throw new ArgumentException("Сумма операции не может быть равна нулю.", nameof(amount));

            BankAccountId = bankAccountId;
            CategoryId = categoryId;
            Amount = amount;
            Date = date;
            Description = description;
        }

        /// <summary>
        /// Выполняет базовую валидацию операции.
        /// </summary>
        /// <returns>True, если операция корректна.</returns>
        public bool Validate()
        {
            // Дополнительные правила проверки можно добавить здесь
            return Amount != 0;
        }

        /// <summary>
        /// Применяет операцию к банковскому счёту.
        /// Если сумма положительная – вызывает Deposit, иначе Withdraw.
        /// </summary>
        /// <param name="account">Банковский счёт.</param>
        public void Apply(BankAccount account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (Amount > 0)
                account.Deposit(Amount);
            else
                account.Withdraw(Math.Abs(Amount));
        }
    }
}
