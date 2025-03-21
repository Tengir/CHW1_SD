using FinancialAccounting.Application.CommandHandlers;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Facade;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Domain.DomainServices;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application.Facade
{
    public class ConsoleFacadeTests
    {
        private IConsoleFacade GetFacade()
        {
            IBankAccountRepository accountRepo = new BankAccountRepository();
            ICategoryRepository categoryRepo = new CategoryRepository();
            IOperationRepository operationRepo = new OperationRepository();

            var accountService = new AccountManagementService(accountRepo);
            var categoryService = new CategoryManagementService(categoryRepo);
            var operationService = new OperationManagementService(operationRepo, accountRepo, new OperationDomainService());

            var createAccountHandler = new CreateAccountCommandHandler(accountService);
            var createCategoryHandler = new CreateCategoryCommandHandler(categoryService);
            var createOperationHandler = new CreateOperationCommandHandler(operationService);
            var updateAccountHandler = new UpdateAccountCommandHandler(accountService);
            var updateCategoryHandler = new UpdateCategoryCommandHandler(categoryService);
            var updateOperationHandler = new UpdateOperationCommandHandler(operationService);
            var deleteAccountHandler = new DeleteAccountCommandHandler(accountService);
            var deleteCategoryHandler = new DeleteCategoryCommandHandler(categoryService);
            var deleteOperationHandler = new DeleteOperationCommandHandler(operationService);

            return new ConsoleFacade(
                accountService,
                categoryService,
                operationService,
                createAccountHandler,
                createCategoryHandler,
                createOperationHandler,
                updateAccountHandler,
                updateCategoryHandler,
                updateOperationHandler,
                deleteAccountHandler,
                deleteCategoryHandler,
                deleteOperationHandler
            );
        }
        
        [Fact]
        public async Task CreateBankAccountAsync_ReturnsValidAccount()
        {
            var facade = GetFacade();
            var command = new CreateAccountCommand { Name = "Facade Test", InitialBalance = 500 };
            AccountDTO dto = await facade.CreateBankAccountAsync(command);
            Assert.NotNull(dto);
            Assert.Equal("Facade Test", dto.Name);
            Assert.Equal(500, dto.Balance);
        }
        
        [Fact]
        public async Task DeleteAccountAsync_DeletesAccount()
        {
            var facade = GetFacade();
            var createCommand = new CreateAccountCommand { Name = "To Delete", InitialBalance = 100 };
            var dto = await facade.CreateBankAccountAsync(createCommand);
            
            await facade.DeleteAccountAsync(dto.Id);
            
            var accounts = await facade.GetAllAccountsAsync();
            Assert.DoesNotContain(accounts, a => a.Id == dto.Id);
        }
    }
}
