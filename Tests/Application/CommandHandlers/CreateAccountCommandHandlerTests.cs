using FinancialAccounting.Application.CommandHandlers;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application.CommandHandlers
{
    public class CreateAccountCommandHandlerTests
    {
        private CreateAccountCommandHandler GetHandler()
        {
            IBankAccountRepository repo = new BankAccountRepository();
            var service = new AccountManagementService(repo);
            return new CreateAccountCommandHandler(service);
        }
        
        [Fact]
        public async Task HandleAsync_CreatesAccount()
        {
            var handler = GetHandler();
            var command = new CreateAccountCommand { Name = "Test Account", InitialBalance = 100 };
            var dto = await handler.HandleAsync(command);
            Assert.NotNull(dto);
            Assert.Equal("Test Account", dto.Name);
            Assert.Equal(100, dto.Balance);
        }
    }
}