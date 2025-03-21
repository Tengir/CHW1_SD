using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды создания категории.
    /// </summary>
    public class CreateCategoryCommandHandler
    {
        private readonly CategoryManagementService _categoryService;
        public CreateCategoryCommandHandler(CategoryManagementService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<CategoryDTO> HandleAsync(CreateCategoryCommand command)
        {
            return await _categoryService.CreateCategoryAsync(command);
        }
    }
}