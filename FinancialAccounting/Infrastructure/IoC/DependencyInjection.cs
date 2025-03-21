using FinancialAccounting.Application.CommandHandlers;
using FinancialAccounting.Application.Facade;
using FinancialAccounting.Application.Services;
using FinancialAccounting.Domain.DomainServices;
using FinancialAccounting.Domain.Repositories;
using FinancialAccounting.Infrastructure.Repositories;
using FinancialAccounting.Infrastructure.Proxy;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialAccounting.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 1. Регистрация репозиториев (in-memory + прокси)
            services.AddSingleton<IBankAccountRepository, BankAccountRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IOperationRepository, OperationRepository>();

            services.Decorate<IBankAccountRepository, ProxyBankAccountRepository>();
            services.Decorate<ICategoryRepository, ProxyCategoryRepository>();
            services.Decorate<IOperationRepository, ProxyOperationRepository>();

            // 2. Регистрация доменных сервисов
            services.AddSingleton<IOperationDomainService, OperationDomainService>();

            // 3. Регистрация Application Services
            services.AddSingleton<AccountManagementService>();
            services.AddSingleton<CategoryManagementService>();
            services.AddSingleton<OperationManagementService>();
            
            // 4. Регистрируем командные хендлеры.
            services.AddSingleton<CreateAccountCommandHandler>();
            services.AddSingleton<CreateCategoryCommandHandler>();
            services.AddSingleton<CreateOperationCommandHandler>();
            services.AddSingleton<UpdateAccountCommandHandler>();
            services.AddSingleton<UpdateCategoryCommandHandler>();
            services.AddSingleton<UpdateOperationCommandHandler>();
            services.AddSingleton<DeleteAccountCommandHandler>();
            services.AddSingleton<DeleteCategoryCommandHandler>();
            services.AddSingleton<DeleteOperationCommandHandler>();
            
            // 5. Регистрация фасада для консоли
            services.AddSingleton<IConsoleFacade, ConsoleFacade>();
            


        }
    }
}
