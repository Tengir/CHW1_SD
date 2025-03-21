using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды удаления категории.
    /// </summary>
    public class DeleteCategoryCommandHandler
    {
        private readonly CategoryManagementService _categoryService;

        public DeleteCategoryCommandHandler(CategoryManagementService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task HandleAsync(DeleteCategoryCommand command)
        {
            await _categoryService.DeleteCategoryAsync(command);
        }
    }
}