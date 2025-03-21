using System;

namespace FinancialAccounting.Domain.Entities
{
    /// <summary>
    /// Базовый класс для всех доменных сущностей.
    /// Содержит общий уникальный идентификатор.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}