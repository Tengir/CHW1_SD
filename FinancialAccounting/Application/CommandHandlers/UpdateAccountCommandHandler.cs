using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды обновления банковского счёта.
    /// </summary>
    public class UpdateAccountCommandHandler
    {
        private readonly AccountManagementService _accountService;
        public UpdateAccountCommandHandler(AccountManagementService accountService)
        {
            _accountService = accountService;
        }
        public async Task<AccountDTO> HandleAsync(UpdateAccountCommand command)
        {
            return await _accountService.UpdateAccountAsync(command);
        }
    }
}