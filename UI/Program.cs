using FinancialAccounting.Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;
using UI.Services;
// Регистрация зависимостей из Infrastructure

namespace UI
{
    /// <summary>
    /// Точка входа консольного приложения.
    /// Здесь настраивается DI-контейнер и запускается главный цикл UI.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            // Создаем коллекцию сервисов и регистрируем все зависимости
            var services = new ServiceCollection();
            DependencyInjection.RegisterServices(services);

            // Регистрируем UI-сервис
            services.AddSingleton<ConsoleUIService>();

            // Собираем провайдер зависимостей
            var serviceProvider = services.BuildServiceProvider();

            // Получаем и запускаем UI-сервис
            var uiService = serviceProvider.GetService<ConsoleUIService>();
            if (uiService != null)
                await uiService.RunAsync();
            else
                Console.WriteLine("UI-сервис не найден!");
        }
    }
}