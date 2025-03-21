using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды удаления операции.
    /// </summary>
    public class DeleteOperationCommandHandler
    {
        private readonly OperationManagementService _operationService;

        public DeleteOperationCommandHandler(OperationManagementService operationService)
        {
            _operationService = operationService;
        }

        public async Task HandleAsync(DeleteOperationCommand command)
        {
            await _operationService.DeleteOperationAsync(command);
        }
    }
}