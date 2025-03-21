using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Factories;
using FinancialAccounting.Domain.Repositories;

namespace FinancialAccounting.Application.Services
{
    /// <summary>
    /// Сервис для управления банковскими счетами.
    /// </summary>
    public class AccountManagementService
    {
        private readonly IBankAccountRepository _accountRepository;

        public AccountManagementService(IBankAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDTO> CreateAccountAsync(CreateAccountCommand command)
        {
            var account = BankAccountFactory.Create(command.Name, command.InitialBalance);
            await _accountRepository.AddAsync(account);
            return new AccountDTO { Id = account.Id, Name = account.Name, Balance = account.Balance };
        }

        public async Task<List<AccountDTO>> GetAllAccountsAsync()
        {
            var accounts = (await _accountRepository.GetAllAsync()).ToList();
            return accounts.Select(a => new AccountDTO { Id = a.Id, Name = a.Name, Balance = a.Balance }).ToList();
        }

        public async Task<AccountDTO> UpdateAccountAsync(UpdateAccountCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId);
            if (account == null)
                throw new Exception("Счет не найден.");

            // Пример обновления: только изменение названия
            account.UpdateDetails(command.NewName);
            await _accountRepository.UpdateAsync(account);
            return new AccountDTO { Id = account.Id, Name = account.Name, Balance = account.Balance };
        }

        public async Task DeleteAccountAsync(DeleteAccountCommand command)
        {
            await _accountRepository.DeleteAsync(command.AccountId);
        }
    }
}
