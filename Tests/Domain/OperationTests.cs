using FinancialAccounting.Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain
{
    public class OperationTests
    {
        [Fact]
        public void Validate_ReturnsTrue_ForNonZeroAmount()
        {
            var op = new Operation(Guid.NewGuid(), Guid.NewGuid(), 50, DateTime.Now);
            Assert.True(op.Validate());
        }

        [Fact]
        public void Apply_ForPositiveAmount_IncreasesBalance()
        {
            var account = new BankAccount("Test Account", 100);
            var op = new Operation(account.Id, Guid.NewGuid(), 50, DateTime.Now);
            op.Apply(account);
            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Apply_ForNegativeAmount_DecreasesBalance()
        {
            var account = new BankAccount("Test Account", 100);
            var op = new Operation(account.Id, Guid.NewGuid(), -30, DateTime.Now);
            op.Apply(account);
            Assert.Equal(70, account.Balance);
        }
    }
}