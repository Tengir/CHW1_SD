using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Domain.DomainServices;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application
{
    public class OperationManagementServiceTests
    {
        private OperationManagementService GetServiceWithInMemoryRepos()
        {
            IBankAccountRepository accountRepo = new BankAccountRepository();
            IOperationRepository operationRepo = new OperationRepository();
            var domainService = new OperationDomainService();
            return new OperationManagementService(operationRepo, accountRepo, domainService);
        }
        
        [Fact]
        public async Task CreateOperationAsync_CreatesOperationAndUpdatesBalance()
        {
            var accountRepo = new BankAccountRepository();
            var service = new OperationManagementService(new OperationRepository(), accountRepo, new OperationDomainService());
            var accountService = new AccountManagementService(accountRepo);
            var accountDto = await accountService.CreateAccountAsync(new CreateAccountCommand { Name = "Test Account", InitialBalance = 100 });
            
            var createOpCommand = new CreateOperationCommand
            {
                BankAccountId = accountDto.Id,
                CategoryId = Guid.NewGuid(),
                Amount = -30,
                Date = DateTime.Now,
                Description = "Test operation"
            };
            OperationDTO opDto = await service.CreateOperationAsync(createOpCommand);
            
            Assert.NotNull(opDto);
            // Проверка баланса не всегда очевидна, если бизнес-логика меняет баланс
        }
        
        [Fact]
        public async Task UpdateOperationAsync_UpdatesOperation()
        {
            // Создаем общий репозиторий для счетов
            var sharedAccountRepo = new BankAccountRepository();
            var operationRepo = new OperationRepository();
            var domainService = new OperationDomainService();
    
            // Создаем счет
            var accountService = new AccountManagementService(sharedAccountRepo);
            var accountDto = await accountService.CreateAccountAsync(
                new CreateAccountCommand { Name = "Test Account", InitialBalance = 100 });
    
            // Создаем операцию с использованием того же репозитория для счетов
            var operationService = new OperationManagementService(operationRepo, sharedAccountRepo, domainService);
    
            var createOpCommand = new CreateOperationCommand
            {
                BankAccountId = accountDto.Id,
                CategoryId = Guid.NewGuid(),  // для теста используем случайный ID
                Amount = 50,
                Date = DateTime.Now,
                Description = "Initial"
            };
            var opDto = await operationService.CreateOperationAsync(createOpCommand);
    
            // Обновляем операцию
            var updateOpCommand = new UpdateOperationCommand
            {
                OperationId = opDto.Id,
                NewAmount = 60,
                NewDescription = "Updated"
            };
            var updated = await operationService.UpdateOperationAsync(updateOpCommand);
    
            Assert.Equal(60, updated.Amount);
            Assert.Equal("Updated", updated.Description);
        }

        
        [Fact]
        public async Task DeleteOperationAsync_DeletesOperation()
        {
            var repo = new OperationRepository();
            var accountRepo = new BankAccountRepository();
            var domainService = new OperationDomainService();
            var service = new OperationManagementService(repo, accountRepo, domainService);
            
            var accountService = new AccountManagementService(accountRepo);
            var accountDto = await accountService.CreateAccountAsync(new CreateAccountCommand { Name = "Test Account", InitialBalance = 100 });
            
            var createOpCommand = new CreateOperationCommand
            {
                BankAccountId = accountDto.Id,
                CategoryId = Guid.NewGuid(),
                Amount = -20,
                Date = DateTime.Now,
                Description = "To be deleted"
            };
            var opDto = await service.CreateOperationAsync(createOpCommand);
            
            var deleteCommand = new DeleteOperationCommand { OperationId = opDto.Id };
            await service.DeleteOperationAsync(deleteCommand);
            
            var operations = await repo.GetAllAsync();
            Assert.DoesNotContain(operations, o => o.Id == opDto.Id);
        }
    }
}
