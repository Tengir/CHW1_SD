using FinancialAccounting.Domain.Entities;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Domain
{
    public class CategoryTests
    {
        [Fact]
        public void UpdateName_ChangesCategoryName()
        {
            var category = new Category("Old Category");
            category.UpdateName("New Category");
            Assert.Equal("New Category", category.Name);
        }
    }
}