using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды создания операции.
    /// </summary>
    public class CreateOperationCommandHandler
    {
        private readonly OperationManagementService _operationService;
        public CreateOperationCommandHandler(OperationManagementService operationService)
        {
            _operationService = operationService;
        }
        public async Task<OperationDTO> HandleAsync(CreateOperationCommand command)
        {
            return await _operationService.CreateOperationAsync(command);
        }
    }
}