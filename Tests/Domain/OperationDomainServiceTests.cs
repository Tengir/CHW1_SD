using FinancialAccounting.Domain.DomainServices;
using FinancialAccounting.Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain
{
    public class OperationDomainServiceTests
    {
        [Fact]
        public async Task ValidateAndApplyOperationAsync_ValidOperation_ReturnsTrueAndUpdatesBalance()
        {
            var account = new BankAccount("Test Account", 100);
            var operation = new Operation(account.Id, Guid.NewGuid(), -30, DateTime.Now);
            var domainService = new OperationDomainService();

            bool result = await domainService.ValidateAndApplyOperationAsync(account, operation);

            Assert.True(result);
            Assert.Equal(70, account.Balance);
        }

        [Fact]
        public async Task ValidateAndApplyOperationAsync_InvalidOperation_ThrowsArgumentException()
        {
            // Arrange
            var account = new BankAccount("Test Account", 100);
            // Здесь попытка создать операцию с суммой 0 должна выбросить исключение уже в конструкторе.
    
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Попытка создания операции с нулевой суммой
                var operation = new Operation(account.Id, Guid.NewGuid(), 0, DateTime.Now);
                var domainService = new OperationDomainService();
                bool result = await domainService.ValidateAndApplyOperationAsync(account, operation);
            });
        }

    }
}