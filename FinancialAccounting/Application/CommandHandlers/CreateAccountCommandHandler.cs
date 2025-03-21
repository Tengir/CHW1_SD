using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды создания банковского счёта.
    /// </summary>
    public class CreateAccountCommandHandler
    {
        private readonly AccountManagementService _accountService;
        public CreateAccountCommandHandler(AccountManagementService accountService)
        {
            _accountService = accountService;
        }
        public async Task<AccountDTO> HandleAsync(CreateAccountCommand command)
        {
            return await _accountService.CreateAccountAsync(command);
        }
    }
}