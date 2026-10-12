Console.WriteLine("Работу выполнили Гонтарева Юлия и Рыжих Вероника");
Console.WriteLine();

Console.Write("Введите имя: ");
string firstName = Console.ReadLine(); // имя

Console.Write("Введите фамилию: ");
string lastName = Console.ReadLine(); // фамилия

Console.Write("Введите год рождения: ");
string birthYearText = Console.ReadLine(); // год рождения (пока строкой)

int birthYear = int.Parse(birthYearText); // переводим в число
int age = DateTime.Now.Year - birthYear;  // считаем возраст

Console.WriteLine($"Добавлен пользователь {firstName} {lastName}, возраст - {age}");    

Console.WriteLine("Введите help, чтобы увидеть список команд.");

string[] todos = new string[2]; // пока массив на 2 задачи
int todoCount = 0;              // сколько задач уже занято

while (true)
{
    Console.Write("> ");
    string input = (Console.ReadLine() ?? "").Trim();

    if (input == "") // пустую строку пропускаем
    {
        continue;
    }

    string command = input.Split(' ')[0]; // берём только первое слово — саму команду
    switch (command)
    {
    // help — шпаргалка по командам
    case "help":
        Console.WriteLine("help    - список всех команд");
        Console.WriteLine("profile - данные пользователя");
        Console.WriteLine("add     - добавить задачу, формат: add \"текст задачи\"");
        Console.WriteLine("view    - показать все задачи");
        Console.WriteLine("exit    - завершить программу");
        break;

    // view — печатаем все непустые задачи
    case "view":
        {
            bool hasTasks = false;
            for (int i = 0; i < todos.Length; i++)
            {
                if (!string.IsNullOrEmpty(todos[i])) // пустые ячейки не показываем
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

    // profile — кто у нас в системе
    case "profile":
        Console.WriteLine($"{firstName} {lastName}, {birthYear}");
        break;

    // add "текст" — кладём задачу в массив
    case "add":
        {
            // режем строку по кавычкам: между ними и есть текст задачи
            string[] parts = input.Split('"');
            if (parts.Length < 3 || parts[1].Trim() == "")
            {
                Console.WriteLine("Ошибка. Формат: add \"текст задачи\"");
                break;
            }

            if (todoCount == todos.Length)
            {
                Console.WriteLine("Нет места в массиве"); // потом сделаем расширение
                break;
            }

            todos[todoCount] = parts[1];
            todoCount++;
            Console.WriteLine("Задача добавлена");
            break;
        }

    // exit — выходим из программы
    case "exit":
        Console.WriteLine("Завершение работы");
        return;

    // всё остальное — не команда
    default:
        Console.WriteLine("Неизвестная команда. Введите help.");
        break;
    }
}