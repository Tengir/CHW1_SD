using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды обновления операции.
    /// </summary>
    public class UpdateOperationCommandHandler
    {
        private readonly OperationManagementService _operationService;
        public UpdateOperationCommandHandler(OperationManagementService operationService)
        {
            _operationService = operationService;
        }
        public async Task<OperationDTO> HandleAsync(UpdateOperationCommand command)
        {
            return await _operationService.UpdateOperationAsync(command);
        }
    }
}