using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.Services;

namespace FinancialAccounting.Application.CommandHandlers
{
    /// <summary>
    /// Обработчик команды удаления банковского счета.
    /// </summary>
    public class DeleteAccountCommandHandler
    {
        private readonly AccountManagementService _accountService;

        public DeleteAccountCommandHandler(AccountManagementService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(DeleteAccountCommand command)
        {
            await _accountService.DeleteAccountAsync(command);
        }
    }
}