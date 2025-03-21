using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Infrastructure.Repositories;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests.Application
{
    public class CategoryManagementServiceTests
    {
        private CategoryManagementService GetServiceWithInMemoryRepo()
        {
            var repo = new CategoryRepository();
            return new CategoryManagementService(repo);
        }
        
        [Fact]
        public async Task CreateCategoryAsync_CreatesCategorySuccessfully()
        {
            var service = GetServiceWithInMemoryRepo();
            var command = new CreateCategoryCommand { Name = "Food" };
            CategoryDTO dto = await service.CreateCategoryAsync(command);
            Assert.NotNull(dto);
            Assert.Equal("Food", dto.Name);
        }
        
        [Fact]
        public async Task UpdateCategoryAsync_UpdatesCategoryName()
        {
            var service = GetServiceWithInMemoryRepo();
            var createCommand = new CreateCategoryCommand { Name = "Old Category" };
            var category = await service.CreateCategoryAsync(createCommand);
            var updateCommand = new UpdateCategoryCommand { CategoryId = category.Id, NewName = "New Category" };
            var updated = await service.UpdateCategoryAsync(updateCommand);
            Assert.Equal("New Category", updated.Name);
        }
        
        [Fact]
        public async Task DeleteCategoryAsync_DeletesCategory()
        {
            var repo = new CategoryRepository();
            var service = new CategoryManagementService(repo);
            var createCommand = new CreateCategoryCommand { Name = "Food" };
            var category = await service.CreateCategoryAsync(createCommand);
            var deleteCommand = new DeleteCategoryCommand { CategoryId = category.Id };
            await service.DeleteCategoryAsync(deleteCommand);
            var categories = await repo.GetAllAsync();
            Assert.DoesNotContain(categories, c => c.Id == category.Id);
        }
    }
}
