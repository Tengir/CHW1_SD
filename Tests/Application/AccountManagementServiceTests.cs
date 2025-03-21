using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application
{
    public class AccountManagementServiceTests
    {
        private AccountManagementService GetServiceWithInMemoryRepo()
        {
            IBankAccountRepository repo = new BankAccountRepository();
            return new AccountManagementService(repo);
        }
        
        [Fact]
        public async Task CreateAccountAsync_CreatesAccountSuccessfully()
        {
            var service = GetServiceWithInMemoryRepo();
            var command = new CreateAccountCommand { Name = "Test Account", InitialBalance = 200 };
            
            AccountDTO dto = await service.CreateAccountAsync(command);
            
            Assert.NotNull(dto);
            Assert.Equal("Test Account", dto.Name);
            Assert.Equal(200, dto.Balance);
        }
        
        [Fact]
        public async Task UpdateAccountAsync_UpdatesAccountName()
        {
            var service = GetServiceWithInMemoryRepo();
            var createCommand = new CreateAccountCommand { Name = "Old Name", InitialBalance = 100 };
            var account = await service.CreateAccountAsync(createCommand);
            
            var updateCommand = new UpdateAccountCommand { AccountId = account.Id, NewName = "New Name" };
            var updatedAccount = await service.UpdateAccountAsync(updateCommand);
            
            Assert.Equal("New Name", updatedAccount.Name);
        }
        
        [Fact]
        public async Task DeleteAccountAsync_DeletesAccount()
        {
            var repo = new BankAccountRepository();
            var service = new AccountManagementService(repo);
            var createCommand = new CreateAccountCommand { Name = "Test Account", InitialBalance = 100 };
            var account = await service.CreateAccountAsync(createCommand);
            
            var deleteCommand = new DeleteAccountCommand { AccountId = account.Id };
            await service.DeleteAccountAsync(deleteCommand);
            
            var accounts = await repo.GetAllAsync();
            Assert.DoesNotContain(accounts, a => a.Id == account.Id);
        }
    }
}
