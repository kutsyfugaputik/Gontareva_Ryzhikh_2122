Console.WriteLine("Работу выполнили Гонтарева Юлия и Рыжих Вероника");
Console.WriteLine();

Console.Write("Введите имя: ");
string firstName = Console.ReadLine(); // чтение имени пользователя

Console.Write("Введите фамилию: ");
string lastName = Console.ReadLine(); // чтение фамилии пользователя

Console.Write("Введите год рождения: ");
string birthYearText = Console.ReadLine(); // чтение года рождения пользователя

int birthYear = int.Parse(birthYearText); // преобразование года рождения в целое число
int age = DateTime.Now.Year - birthYear; // вычисление возраста пользователя

Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");    

Console.WriteLine("Введите help, чтобы увидеть список команд.");

string[] todos = new string[2]; // массив задач с небольшой начальной длиной
int todoCount = 0;              // количество реально добавленных задач

while (true)
{
    Console.Write("> ");
    string input = (Console.ReadLine() ?? "").Trim();

    if (input == "")
    {
        continue;
    }

    string command = input.Split(' ')[0]; // первое слово - название команды
    switch (command)
    {
    // Команда "help" — выводит список всех доступных команд
    case "help":
        Console.WriteLine("help    - список всех команд");
        Console.WriteLine("profile - данные пользователя");
        Console.WriteLine("add     - добавить задачу, формат: add \"текст задачи\"");
        Console.WriteLine("view    - показать все задачи");
        Console.WriteLine("exit    - завершить программу");
        break;

    case "view":
        {
            bool hasTasks = false;
            for (int i = 0; i < todos.Length; i++)
            {
                if (!string.IsNullOrEmpty(todos[i])) // только непустые элементы
                {
                    Console.WriteLine($"{i + 1}. {todos[i]}");
                    hasTasks = true;
                }
            }
            if (!hasTasks)
            {
                Console.WriteLine("Список задач пуст");
            }
            break;
        }

    // Команда "profile" — выводит данные пользователя
    case "profile":
        Console.WriteLine($"{firstName} {lastName}, {birthYear}");
        break;
    case "add":
        {
            // add "текст задачи": разбиваем по кавычкам, текст оказывается в parts[1]
            string[] parts = input.Split('"');
            if (parts.Length < 3 || parts[1].Trim() == "")
            {
                Console.WriteLine("Ошибка. Формат: add \"текст задачи\"");
                break;
            }

            if (todoCount == todos.Length)
            {
                Console.WriteLine("Нет места в массиве"); // временно, заменим на расширение
                break;
            }

            todos[todoCount] = parts[1];
            todoCount++;
            Console.WriteLine("Задача добавлена");
            break;
        }

    // Команда "exit" — завершает работу программы
    case "exit":
        Console.WriteLine("Завершение работы");
        return; // выход из метода/программы

    // Любая другая команда — сообщение об ошибке
    default:
        Console.WriteLine("Неизвестная команда. Введите help.");
        break;
    }
}
