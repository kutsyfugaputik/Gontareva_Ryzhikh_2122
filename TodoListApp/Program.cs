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
void ShowHelp()
{
    Console.WriteLine("help    - список всех команд");
    Console.WriteLine("profile - данные пользователя");
    Console.WriteLine("add     - добавить задачу, формат: add \"текст задачи\"");
    Console.WriteLine("view    - показать все задачи");
    Console.WriteLine("exit    - завершить программу");
}

void ShowProfile()
{
    Console.WriteLine($"{firstName} {lastName}, {birthYear}");
}

void ViewTasks()
{
    bool hasTasks = false;
    for (int i = 0; i < todoCount; i++)
    {
        Console.WriteLine($"{i + 1}. {todos[i]}");
        hasTasks = true;
    }
    if (!hasTasks)
    {
        Console.WriteLine("Список задач пуст");
    }
}
void ExpandArrays()
{
    string[] newTodos = new string[todos.Length * GrowthFactor];
    for (int i = 0; i < todos.Length; i++)
    {
        newTodos[i] = todos[i];
    }
    todos = newTodos;
    Console.WriteLine($"(массив расширен до {todos.Length})");
}
void AddTask(string args)
{
    // режем строку по кавычкам: между ними и есть текст задачи
    string[] parts = args.Split('"');
    if (parts.Length < 3 || parts[1].Trim() == "")
    {
        Console.WriteLine("Ошибка. Формат: add \"текст задачи\"");
        return;
    }
    // если места нет — расширяем массив
    if (todoCount == todos.Length) ExpandArrays();
    todos[todoCount] = parts[1];
    todoCount++;
    Console.WriteLine("Задача добавлена");
}
while (true)
{
    Console.Write("> ");
    string input = (Console.ReadLine() ?? "").Trim();
    if (input == "") // пустую строку пропускаем
    {
        continue;
    }
    string command = input.Split(' ')[0]; // берём только первое слово — саму команду
    string args = input.Length > command.Length ? input.Substring(command.Length).Trim() : "";
        switch (command)
    {
        case CommandHelp:    ShowHelp(); break;
        case CommandProfile: ShowProfile(); break;
        case CommandAdd:     AddTask(args); break;
        case CommandView:    ViewTasks(); break;
        case CommandExit:
            Console.WriteLine("Завершение работы");
            return;
        default:
            Console.WriteLine("Неизвестная команда. Введите help.");
            break;
    }
}

