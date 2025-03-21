using FinancialAccounting.Domain.Factories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain
{
    public class FactoryTests
    {
        [Fact]
        public void BankAccountFactory_CreatesValidAccount()
        {
            var account = BankAccountFactory.Create("Test", 100);
            Assert.NotEqual(Guid.Empty, account.Id);
            Assert.Equal("Test", account.Name);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void CategoryFactory_CreatesValidCategory()
        {
            var category = CategoryFactory.Create("Food");
            Assert.NotEqual(Guid.Empty, category.Id);
            Assert.Equal("Food", category.Name);
        }

        [Fact]
        public void OperationFactory_CreatesValidOperation()
        {
            var op = OperationFactory.Create(Guid.NewGuid(), Guid.NewGuid(), 50, DateTime.Today, "Test");
            Assert.NotEqual(Guid.Empty, op.Id);
            Assert.Equal(50, op.Amount);
            Assert.Equal("Test", op.Description);
        }
    }
}