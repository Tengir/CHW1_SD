using FinancialAccounting.Application.Commands;
using FinancialAccounting.Application.DTOs;
using FinancialAccounting.Application.Facade;
using UI.Helpers;
// Используем IConsoleFacade

namespace UI.Services
{
    /// <summary>
    /// Сервис консольного пользовательского интерфейса.
    /// Отвечает за отображение главного меню, подменю для управления счетами, категориями и операциями,
    /// а также за просмотр данных.
    /// </summary>
    public class ConsoleUIService
    {
        private readonly IConsoleFacade _facade;

        public ConsoleUIService(IConsoleFacade facade)
        {
            _facade = facade;
        }

        /// <summary>
        /// Главный цикл приложения: вывод главного меню и обработка выбора пользователя.
        /// </summary>
        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Управление счетами");
                Console.WriteLine("2. Управление категориями");
                Console.WriteLine("3. Управление операциями");
                Console.WriteLine("4. Просмотр данных");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите опцию: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        await ManageAccountsAsync();
                        break;
                    case "2":
                        await ManageCategoriesAsync();
                        break;
                    case "3":
                        await ManageOperationsAsync();
                        break;
                    case "4":
                        await ViewDataAsync();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу для повторного ввода...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #region Управление счетами

        private async Task ManageAccountsAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление счетами ===");
                Console.WriteLine("1. Создать новый счет");
                Console.WriteLine("2. Список счетов");
                Console.WriteLine("3. Редактировать счет");
                Console.WriteLine("4. Удалить счет");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateAccountAsync();
                        break;
                    case "2":
                        await ListAccountsAsync(showDetails: true);
                        break;
                    case "3":
                        await EditAccountAsync();
                        break;
                    case "4":
                        await DeleteAccountAsync();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task CreateAccountAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Создание нового счета ===");
            Console.Write("Введите название счета: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var balance = InputHelper.ReadDecimal("Введите начальный баланс: ");
            var command = new CreateAccountCommand { Name = name, InitialBalance = balance };

            try
            {
                AccountDTO dto = await _facade.CreateBankAccountAsync(command);
                Console.WriteLine($"Счет создан. ID: {dto.Id}, Название: {dto.Name}, Баланс: {dto.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании счета: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private async Task ListAccountsAsync(bool showDetails)
        {
            Console.Clear();
            Console.WriteLine("=== Список счетов ===");
            List<AccountDTO> accounts = await _facade.GetAllAccountsAsync();

            if (accounts.Count == 0)
            {
                Console.WriteLine("Счетов не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }

            if (showDetails)
            {
                Console.WriteLine("Введите номер счета для просмотра деталей, или 0 для возврата:");
                int selection = InputHelper.ReadInt("Ваш выбор: ");
                if (selection == 0)
                    return;
                if (selection < 1 || selection > accounts.Count)
                {
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                    Console.ReadKey();
                    return;
                }
                var account = accounts[selection - 1];
                Console.WriteLine($"Детали счета: ID: {account.Id}, Название: {account.Name}, Баланс: {account.Balance}");
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
        }

        private async Task EditAccountAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование счета ===");
            List<AccountDTO> accounts = await _facade.GetAllAccountsAsync();
            if (accounts.Count == 0)
            {
                Console.WriteLine("Счетов не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }
            Console.WriteLine("Введите номер счета для редактирования, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > accounts.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var selectedAccount = accounts[selection - 1];
            Console.Write("Введите новое название счета: ");
            var newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Новое название не может быть пустым. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var command = new UpdateAccountCommand { AccountId = selectedAccount.Id, NewName = newName };
            try
            {
                var updated = await _facade.UpdateAccountAsync(command);
                Console.WriteLine($"Счет обновлен. ID: {updated.Id}, Новое название: {updated.Name}, Баланс: {updated.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении счета: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        private async Task DeleteAccountAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление счета ===");
            List<AccountDTO> accounts = await _facade.GetAllAccountsAsync();
            if (accounts.Count == 0)
            {
                Console.WriteLine("Счетов не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }
            Console.WriteLine("Введите номер счета для удаления, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > accounts.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var account = accounts[selection - 1];
            Console.Write($"Вы уверены, что хотите удалить счет '{account.Name}'? (Y/N): ");
            var confirmation = Console.ReadLine();
            if (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                return;
            try
            {
                await _facade.DeleteAccountAsync(account.Id);
                Console.WriteLine("Счет удален.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении счета: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        #endregion

        #region Управление категориями

        private async Task ManageCategoriesAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление категориями ===");
                Console.WriteLine("1. Создать новую категорию");
                Console.WriteLine("2. Список категорий");
                Console.WriteLine("3. Редактировать категорию");
                Console.WriteLine("4. Удалить категорию");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateCategoryAsync();
                        break;
                    case "2":
                        await ListCategoriesAsync(showDetails: true);
                        break;
                    case "3":
                        await EditCategoryAsync();
                        break;
                    case "4":
                        await DeleteCategoryAsync();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task CreateCategoryAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Создание новой категории ===");
            Console.Write("Введите название категории: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Название не может быть пустым. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var command = new CreateCategoryCommand { Name = name };
            try
            {
                CategoryDTO dto = await _facade.CreateCategoryAsync(command);
                Console.WriteLine($"Категория создана. ID: {dto.Id}, Название: {dto.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании категории: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private async Task ListCategoriesAsync(bool showDetails)
        {
            Console.Clear();
            Console.WriteLine("=== Список категорий ===");
            List<CategoryDTO> categories = await _facade.GetAllCategoriesAsync();
            if (categories.Count == 0)
            {
                Console.WriteLine("Категорий не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            if (showDetails)
            {
                Console.WriteLine("Введите номер категории для просмотра деталей, или 0 для возврата:");
                int selection = InputHelper.ReadInt("Ваш выбор: ");
                if (selection == 0)
                    return;
                if (selection < 1 || selection > categories.Count)
                {
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                    Console.ReadKey();
                    return;
                }
                var category = categories[selection - 1];
                Console.WriteLine($"Детали категории: ID: {category.Id}, Название: {category.Name}");
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
        }

        private async Task EditCategoryAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование категории ===");
            List<CategoryDTO> categories = await _facade.GetAllCategoriesAsync();
            if (categories.Count == 0)
            {
                Console.WriteLine("Категорий не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            Console.WriteLine("Введите номер категории для редактирования, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > categories.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var selectedCategory = categories[selection - 1];
            Console.Write("Введите новое название категории: ");
            var newName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Новое название не может быть пустым. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var command = new UpdateCategoryCommand { CategoryId = selectedCategory.Id, NewName = newName };
            try
            {
                var updated = await _facade.UpdateCategoryAsync(command);
                Console.WriteLine($"Категория обновлена. ID: {updated.Id}, Новое название: {updated.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении категории: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        private async Task DeleteCategoryAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление категории ===");
            List<CategoryDTO> categories = await _facade.GetAllCategoriesAsync();
            if (categories.Count == 0)
            {
                Console.WriteLine("Категорий не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            Console.WriteLine("Введите номер категории для удаления, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > categories.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var category = categories[selection - 1];
            Console.Write($"Вы уверены, что хотите удалить категорию '{category.Name}'? (Y/N): ");
            var confirmation = Console.ReadLine();
            if (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                return;
            try
            {
                await _facade.DeleteCategoryAsync(category.Id);
                Console.WriteLine("Категория удалена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении категории: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        #endregion

        #region Управление операциями

        private async Task ManageOperationsAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управление операциями ===");
                Console.WriteLine("1. Создать новую операцию");
                Console.WriteLine("2. Список операций");
                Console.WriteLine("3. Редактировать операцию");
                Console.WriteLine("4. Удалить операцию");
                Console.WriteLine("5. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateOperationAsync();
                        break;
                    case "2":
                        await ListOperationsAsync(showDetails: true);
                        break;
                    case "3":
                        await EditOperationAsync();
                        break;
                    case "4":
                        await DeleteOperationAsync();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private async Task CreateOperationAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Создание новой операции ===");

            // Выбор счета из списка
            Console.WriteLine("Выберите счет из списка:");
            List<AccountDTO> accounts = await _facade.GetAllAccountsAsync();
            if (accounts.Count == 0)
            {
                Console.WriteLine("Нет доступных счетов. Нажмите любую клавишу для возврата...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i].Name} (Баланс: {accounts[i].Balance})");
            }
            Console.WriteLine("Введите номер счета (или 0 для отмены): ");
            int accountSelection = InputHelper.ReadInt("Ваш выбор: ");
            if (accountSelection == 0)
                return;
            if (accountSelection < 1 || accountSelection > accounts.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var selectedAccount = accounts[accountSelection - 1];

            // Выбор категории из списка
            Console.WriteLine("Выберите категорию из списка:");
            List<CategoryDTO> categories = await _facade.GetAllCategoriesAsync();
            if (categories.Count == 0)
            {
                Console.WriteLine("Нет доступных категорий. Нажмите любую клавишу для возврата...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            Console.WriteLine("Введите номер категории (или 0 для отмены): ");
            int categorySelection = InputHelper.ReadInt("Ваш выбор: ");
            if (categorySelection == 0)
                return;
            if (categorySelection < 1 || categorySelection > categories.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var selectedCategory = categories[categorySelection - 1];

            // Ввод суммы операции
            var amount = InputHelper.ReadDecimal("Введите сумму операции (положительное – доход, отрицательное – расход): ");

            // Ввод даты операции
            var date = InputHelper.ReadDateTime("Введите дату операции (YYYY-MM-DD): ");

            // Ввод описания
            Console.Write("Введите описание операции (опционально): ");
            var description = Console.ReadLine();

            var command = new CreateOperationCommand
            {
                BankAccountId = selectedAccount.Id,
                CategoryId = selectedCategory.Id,
                Amount = amount,
                Date = date,
                Description = description
            };

            try
            {
                var dto = await _facade.CreateOperationAsync(command);
                Console.WriteLine($"Операция создана. ID: {dto.Id}, Сумма: {dto.Amount}, Дата: {dto.Date}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании операции: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private async Task ListOperationsAsync(bool showDetails)
        {
            Console.Clear();
            Console.WriteLine("=== Список операций ===");
            List<OperationDTO> operations = await _facade.GetAllOperationsAsync();
            if (operations.Count == 0)
            {
                Console.WriteLine("Операций не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < operations.Count; i++)
            {
                // Здесь можно отобразить краткую информацию об операции
                Console.WriteLine($"{i + 1}. Счет: {operations[i].BankAccountId}, Категория: {operations[i].CategoryId}, Сумма: {operations[i].Amount}, Дата: {operations[i].Date.ToShortDateString()}");
            }
            if (showDetails)
            {
                Console.WriteLine("Введите номер операции для просмотра деталей, или 0 для возврата:");
                int selection = InputHelper.ReadInt("Ваш выбор: ");
                if (selection == 0)
                    return;
                if (selection < 1 || selection > operations.Count)
                {
                    Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                    Console.ReadKey();
                    return;
                }
                var operation = operations[selection - 1];
                Console.WriteLine($"Детали операции:\nID: {operation.Id}\nСчет: {operation.BankAccountId}\nКатегория: {operation.CategoryId}\nСумма: {operation.Amount}\nДата: {operation.Date}\nОписание: {operation.Description}");
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Нажмите любую клавишу для возврата...");
                Console.ReadKey();
            }
        }

        private async Task EditOperationAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Редактирование операции ===");
            List<OperationDTO> operations = await _facade.GetAllOperationsAsync();
            if (operations.Count == 0)
            {
                Console.WriteLine("Операций не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < operations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Счет: {operations[i].BankAccountId}, Категория: {operations[i].CategoryId}, Сумма: {operations[i].Amount}");
            }
            Console.WriteLine("Введите номер операции для редактирования, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > operations.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var selectedOperation = operations[selection - 1];

            // Предположим, что редактирование операции ограничено изменением суммы и описания
            var newAmount = InputHelper.ReadDecimal("Введите новую сумму операции: ");
            Console.Write("Введите новое описание операции: ");
            var newDescription = Console.ReadLine();

            var command = new UpdateOperationCommand
            {
                OperationId = selectedOperation.Id,
                NewAmount = newAmount,
                NewDescription = newDescription
            };
            try
            {
                var updated = await _facade.UpdateOperationAsync(command);
                Console.WriteLine($"Операция обновлена. ID: {updated.Id}, Новая сумма: {updated.Amount}, Новое описание: {updated.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении операции: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        private async Task DeleteOperationAsync()
        {
            Console.Clear();
            Console.WriteLine("=== Удаление операции ===");
            List<OperationDTO> operations = await _facade.GetAllOperationsAsync();
            if (operations.Count == 0)
            {
                Console.WriteLine("Операций не найдено. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            for (int i = 0; i < operations.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Счет: {operations[i].BankAccountId}, Категория: {operations[i].CategoryId}, Сумма: {operations[i].Amount}");
            }
            Console.WriteLine("Введите номер операции для удаления, или 0 для отмены:");
            int selection = InputHelper.ReadInt("Ваш выбор: ");
            if (selection == 0)
                return;
            if (selection < 1 || selection > operations.Count)
            {
                Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                Console.ReadKey();
                return;
            }
            var operation = operations[selection - 1];
            Console.Write($"Вы уверены, что хотите удалить операцию с ID {operation.Id}? (Y/N): ");
            var confirmation = Console.ReadLine();
            if (!confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                return;
            try
            {
                await _facade.DeleteOperationAsync(operation.Id);
                Console.WriteLine("Операция удалена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении операции: {ex.Message}");
            }
            Console.WriteLine("Нажмите любую клавишу для возврата...");
            Console.ReadKey();
        }

        #endregion

        #region Просмотр данных

        private async Task ViewDataAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Просмотр данных ===");
                Console.WriteLine("1. Список счетов с балансами");
                Console.WriteLine("2. Список категорий");
                Console.WriteLine("3. Список операций");
                Console.WriteLine("4. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ListAccountsAsync(showDetails: false);
                        break;
                    case "2":
                        await ListCategoriesAsync(showDetails: false);
                        break;
                    case "3":
                        await ListOperationsAsync(showDetails: true);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        #endregion
    }
}
