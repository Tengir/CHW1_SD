using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.CommandHandlers;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.Facade
{
    /// <summary>
    /// Фасад для консольного приложения, использующий паттерн Команда с отдельными обработчиками.
    /// Делегирует выполнение команд соответствующим обработчикам.
    /// </summary>
    public class ConsoleFacade : IConsoleFacade
    {
        private readonly CreateAccountCommandHandler _createAccountCommandHandler;
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler;
        private readonly CreateOperationCommandHandler _createOperationCommandHandler;
        private readonly UpdateAccountCommandHandler _updateAccountCommandHandler;
        private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler;
        private readonly UpdateOperationCommandHandler _updateOperationCommandHandler;
        private readonly DeleteAccountCommandHandler _deleteAccountCommandHandler;
        private readonly DeleteCategoryCommandHandler _deleteCategoryCommandHandler;
        private readonly DeleteOperationCommandHandler _deleteOperationCommandHandler;

        private readonly AccountManagementService _accountService;
        private readonly CategoryManagementService _categoryService;
        private readonly OperationManagementService _operationService;

        public ConsoleFacade(AccountManagementService accountService,
                             CategoryManagementService categoryService,
                             OperationManagementService operationService,
                             CreateAccountCommandHandler createAccountCommandHandler,
                             CreateCategoryCommandHandler createCategoryCommandHandler,
                             CreateOperationCommandHandler createOperationCommandHandler,
                             UpdateAccountCommandHandler updateAccountCommandHandler,
                             UpdateCategoryCommandHandler updateCategoryCommandHandler,
                             UpdateOperationCommandHandler updateOperationCommandHandler,
                             DeleteAccountCommandHandler deleteAccountCommandHandler,
                             DeleteCategoryCommandHandler deleteCategoryCommandHandler,
                             DeleteOperationCommandHandler deleteOperationCommandHandler)
        {
            _accountService = accountService;
            _categoryService = categoryService;
            _operationService = operationService;

            _createAccountCommandHandler = createAccountCommandHandler;
            _createCategoryCommandHandler = createCategoryCommandHandler;
            _createOperationCommandHandler = createOperationCommandHandler;
            _updateAccountCommandHandler = updateAccountCommandHandler;
            _updateCategoryCommandHandler = updateCategoryCommandHandler;
            _updateOperationCommandHandler = updateOperationCommandHandler;
            _deleteAccountCommandHandler = deleteAccountCommandHandler;
            _deleteCategoryCommandHandler = deleteCategoryCommandHandler;
            _deleteOperationCommandHandler = deleteOperationCommandHandler;
        }

        // Методы создания через обработчики команд
        public async Task<AccountDTO> CreateBankAccountAsync(CreateAccountCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Название счета не может быть пустым.");
            if (command.InitialBalance < 0)
                throw new ArgumentException("Начальный баланс не может быть отрицательным.");

            return await _createAccountCommandHandler.HandleAsync(command);
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CreateCategoryCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Название категории не может быть пустым.");

            return await _createCategoryCommandHandler.HandleAsync(command);
        }

        public async Task<OperationDTO> CreateOperationAsync(CreateOperationCommand command)
        {
            if (command.Amount == 0)
                throw new ArgumentException("Сумма операции не может быть равна нулю.");

            return await _createOperationCommandHandler.HandleAsync(command);
        }

        // Методы получения списков (оставляем вызовы сервисов напрямую, т.к. они не оборачиваются в команды)
        public async Task<List<AccountDTO>> GetAllAccountsAsync() =>
            await _accountService.GetAllAccountsAsync();

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync() =>
            await _categoryService.GetAllCategoriesAsync();

        public async Task<List<OperationDTO>> GetAllOperationsAsync() =>
            await _operationService.GetAllOperationsAsync();

        // Методы обновления через обработчики команд
        public async Task<AccountDTO> UpdateAccountAsync(UpdateAccountCommand command) =>
            await _updateAccountCommandHandler.HandleAsync(command);

        public async Task<CategoryDTO> UpdateCategoryAsync(UpdateCategoryCommand command) =>
            await _updateCategoryCommandHandler.HandleAsync(command);

        public async Task<OperationDTO> UpdateOperationAsync(UpdateOperationCommand command) =>
            await _updateOperationCommandHandler.HandleAsync(command);

        // Методы удаления через обработчики команд
        public async Task DeleteAccountAsync(Guid accountId)
        {
            var command = new DeleteAccountCommand { AccountId = accountId };
            await _deleteAccountCommandHandler.HandleAsync(command);
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var command = new DeleteCategoryCommand { CategoryId = categoryId };
            await _deleteCategoryCommandHandler.HandleAsync(command);
        }

        public async Task DeleteOperationAsync(Guid operationId)
        {
            var command = new DeleteOperationCommand { OperationId = operationId };
            await _deleteOperationCommandHandler.HandleAsync(command);
        }
    }
}
