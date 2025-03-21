using FinancialAccounting.Domain.Entities;

namespace FinancialAccounting.Domain.Factories
{
    /// <summary>
    /// Фабрика для создания объектов Category.
    /// </summary>
    public static class CategoryFactory
    {
        /// <summary>
        /// Создаёт новую категорию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <returns>Новый объект Category.</returns>
        public static Category Create(string name)
        {
            return new Category(name);
        }
    }
}