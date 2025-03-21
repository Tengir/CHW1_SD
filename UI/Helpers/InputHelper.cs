namespace UI.Helpers
{
    /// <summary>
    /// Вспомогательный класс для чтения и проверки пользовательского ввода.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Считывает значение типа decimal с консоли, запрашивая повторный ввод при неверном формате.
        /// </summary>
        public static decimal ReadDecimal(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var input = Console.ReadLine();
                if (decimal.TryParse(input, out decimal result))
                    return result;
                Console.Write("Некорректное значение, попробуйте снова: ");
            }
        }

        /// <summary>
        /// Считывает значение типа int с консоли, запрашивая повторный ввод при неверном формате.
        /// </summary>
        public static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                    return result;
                Console.Write("Некорректное значение, попробуйте снова: ");
            }
        }

        /// <summary>
        /// Считывает значение типа Guid с консоли, запрашивая повторный ввод при неверном формате.
        /// </summary>
        public static Guid ReadGuid(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var input = Console.ReadLine();
                if (Guid.TryParse(input, out Guid result))
                    return result;
                Console.Write("Некорректный GUID, попробуйте снова: ");
            }
        }

        /// <summary>
        /// Считывает дату с консоли, запрашивая повторный ввод при неверном формате.
        /// </summary>
        public static DateTime ReadDateTime(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                var input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime result))
                    return result;
                Console.Write("Некорректный формат даты, попробуйте снова: ");
            }
        }
    }
}
