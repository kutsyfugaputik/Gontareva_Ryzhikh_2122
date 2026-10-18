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
const int InitialCapacity = 2;   // начальная длина массива
const int GrowthFactor = 2;      // во сколько раз растёт массив
const string CommandHelp = "help";
const string CommandProfile = "profile";
const string CommandAdd = "add";
const string CommandView = "view";
const string CommandExit = "exit";
string[] todos = new string[InitialCapacity]; // пока массив на 2 задачи
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
        case CommandHelp:
            Console.WriteLine("help    - список всех команд");
            Console.WriteLine("profile - данные пользователя");
            Console.WriteLine("add     - добавить задачу, формат: add \"текст задачи\"");
            Console.WriteLine("view    - показать все задачи");
            Console.WriteLine("exit    - завершить программу");
            break;
        // view — печатаем все непустые задачи
        case CommandView:
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
        case CommandProfile:
            Console.WriteLine($"{firstName} {lastName}, {birthYear}");
            break;
        // add "текст" — кладём задачу в массив
        case CommandAdd:
            {
                // режем строку по кавычкам: между ними и есть текст задачи
                string[] parts = input.Split('"');
                if (parts.Length < 3 || parts[1].Trim() == "")
                {
                    Console.WriteLine("Ошибка. Формат: add \"текст задачи\"");
                    break;
                }
                // если места нет — расширяем массив в 2 раза
                if (todoCount == todos.Length)
                {
                    string[] newTodos = new string[todos.Length * GrowthFactor]; // новый массив вдвое длиннее
                    for (int i = 0; i < todos.Length; i++)
                    {
                        newTodos[i] = todos[i]; // переписываем всё старое
                    }
                    todos = newTodos; // теперь todos указывает на новый массив
                    Console.WriteLine($"(массив расширен до {todos.Length})");
                }
                todos[todoCount] = parts[1];
                todoCount++;
                Console.WriteLine("Задача добавлена");
                break;
            }
        // exit — выходим из программы
        case CommandExit:
            Console.WriteLine("Завершение работы");
            return;
        // всё остальное — не команда
        case CommandDefault:
            Console.WriteLine("Неизвестная команда. Введите help.");
            break;
    }
}

