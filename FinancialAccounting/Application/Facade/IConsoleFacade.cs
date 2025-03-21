using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;

namespace FinancialAccounting.Application.Facade
{
    /// <summary>
    /// Интерфейс фасада, ориентированного на консольное приложение.
    /// Предоставляет методы для создания, получения, обновления и удаления сущностей.
    /// </summary>
    public interface IConsoleFacade
    {
        // Методы создания
        Task<AccountDTO> CreateBankAccountAsync(CreateAccountCommand command);
        Task<CategoryDTO> CreateCategoryAsync(CreateCategoryCommand command);
        Task<OperationDTO> CreateOperationAsync(CreateOperationCommand command);

        // Методы получения списков сущностей
        Task<List<AccountDTO>> GetAllAccountsAsync();
        Task<List<CategoryDTO>> GetAllCategoriesAsync();
        Task<List<OperationDTO>> GetAllOperationsAsync();

        // Методы обновления
        Task<AccountDTO> UpdateAccountAsync(UpdateAccountCommand command);
        Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryCommand command);
        Task<OperationDTO> UpdateOperationAsync(UpdateOperationCommand command);

        // Методы удаления
        Task DeleteAccountAsync(Guid accountId);
        Task DeleteCategoryAsync(Guid categoryId);
        Task DeleteOperationAsync(Guid operationId);
    }
}