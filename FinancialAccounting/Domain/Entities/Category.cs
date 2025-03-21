using System;

namespace FinancialAccounting.Domain.Entities
{
    /// <summary>
    /// Представляет категорию операций (например, "Кафе", "Зарплата").
    /// Категории используются для классификации операций и не зависят от банковского счёта.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Конструктор создания категории.
        /// </summary>
        /// <param name="name">Название категории.</param>
        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название категории не может быть пустым.", nameof(name));

            Name = name;
        }

        /// <summary>
        /// Обновляет название категории.
        /// </summary>
        /// <param name="newName">Новое название.</param>
        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Новое название категории не может быть пустым.", nameof(newName));

            Name = newName;
        }
    }
}