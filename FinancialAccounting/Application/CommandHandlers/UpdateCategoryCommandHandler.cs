using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды обновления категории.
    /// </summary>
    public class UpdateCategoryCommandHandler
    {
        private readonly CategoryManagementService _categoryService;
        public UpdateCategoryCommandHandler(CategoryManagementService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<CategoryDTO> HandleAsync(UpdateCategoryCommand command)
        {
            return await _categoryService.UpdateCategoryAsync(command);
        }
    }
}