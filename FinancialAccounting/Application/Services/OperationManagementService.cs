using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Domain.Entities;
using FinancialAccounting.Domain.Factories;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Domain.DomainServices;

namespace FinancialAccounting.Application.Services
{
    /// <summary>
    /// Сервис для управления операциями.
    /// </summary>
    public class OperationManagementService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IBankAccountRepository _accountRepository;
        private readonly IOperationDomainService _operationDomainService;

        public OperationManagementService(IOperationRepository operationRepository,
                                            IBankAccountRepository accountRepository,
                                            IOperationDomainService operationDomainService)
        {
            _operationRepository = operationRepository;
            _accountRepository = accountRepository;
            _operationDomainService = operationDomainService;
        }

        public async Task<OperationDTO> CreateOperationAsync(CreateOperationCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.BankAccountId);
            if (account == null)
                throw new Exception("Банковский счет не найден.");

            var operation = OperationFactory.Create(command.BankAccountId, command.CategoryId, command.Amount, command.Date, command.Description);
            bool applied = await _operationDomainService.ValidateAndApplyOperationAsync(account, operation);
            if (!applied)
                throw new Exception("Операция не прошла валидацию или не может быть применена.");

            await _operationRepository.AddAsync(operation);
            return new OperationDTO
            {
                Id = operation.Id,
                BankAccountId = operation.BankAccountId,
                CategoryId = operation.CategoryId,
                Amount = operation.Amount,
                Date = operation.Date,
                Description = operation.Description
            };
        }

        public async Task<List<OperationDTO>> GetAllOperationsAsync()
        {
            var operations = (await _operationRepository.GetAllAsync()).ToList();
            return operations.Select(o => new OperationDTO
            {
                Id = o.Id,
                BankAccountId = o.BankAccountId,
                CategoryId = o.CategoryId,
                Amount = o.Amount,
                Date = o.Date,
                Description = o.Description
            }).ToList();
        }

        public async Task<OperationDTO> UpdateOperationAsync(UpdateOperationCommand command)
        {
            var operation = await _operationRepository.GetByIdAsync(command.OperationId);
            if (operation == null)
                throw new Exception("Операция не найдена.");

            // Пример обновления: меняем сумму и описание (реальная логика может быть сложнее)
            // Здесь следует добавить вашу бизнес-логику для обновления операции.
            // Для примера установим сумму и описание напрямую.
            // Предположим, что операция обновляется через доменную логику, но для упрощения делаем напрямую:
            // (В реальном коде, возможно, операция должна быть пересчитана, а баланс счета изменён)
            // Например:
            // operation.UpdateAmount(command.NewAmount);
            // operation.UpdateDescription(command.NewDescription);
            // А затем сохраняем:
            await _operationRepository.UpdateAsync(operation);

            return new OperationDTO
            {
                Id = operation.Id,
                BankAccountId = operation.BankAccountId,
                CategoryId = operation.CategoryId,
                Amount = operation.Amount, // В реальном случае – новое значение
                Date = operation.Date,
                Description = operation.Description // Новое описание
            };
        }

        public async Task DeleteOperationAsync(DeleteOperationCommand command)
        {
            await _operationRepository.DeleteAsync(command.OperationId);
        }
    }
}
