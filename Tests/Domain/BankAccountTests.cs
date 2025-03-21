using FinancialAccounting.Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain
{
    public class BankAccountTests
    {
        [Fact]
        public void Deposit_IncreasesBalance()
        {
            // Arrange
            var account = new BankAccount("Test Account", 100);
            decimal depositAmount = 50;

            // Act
            account.Deposit(depositAmount);

            // Assert
            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Withdraw_DecreasesBalance()
        {
            var account = new BankAccount("Test Account", 100);
            account.Withdraw(30);
            Assert.Equal(70, account.Balance);
        }

        [Fact]
        public void Withdraw_InsufficientFunds_ThrowsException()
        {
            var account = new BankAccount("Test Account", 100);
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(150));
        }

        [Fact]
        public void UpdateDetails_ChangesName()
        {
            var account = new BankAccount("Old Name", 100);
            account.UpdateDetails("New Name");
            Assert.Equal("New Name", account.Name);
        }
    }
}