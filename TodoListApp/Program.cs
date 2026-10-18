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
const string CommandDone = "done";
const string DoneText = "сделано";
const string NotDoneText = "не сделано";
string[] todos = new string[InitialCapacity]; // пока массив на 2 задачи
int todoCount = 0;              // сколько задач уже занято
bool[] statuses = new bool[InitialCapacity];       // выполнена ли задача
DateTime[] dates = new DateTime[InitialCapacity];  // дата создания / изменения
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
    if (todoCount == 0)
    {
        Console.WriteLine("Список задач пуст");
        return;
    }
    for (int i = 0; i < todoCount; i++)
    {
        string status = statuses[i] ? DoneText : NotDoneText;
        Console.WriteLine($"{i + 1} {todos[i]} {status} {dates[i]}");
    }
}
T[] ExpandArray<T>(T[] oldArray, int newSize)
{
    T[] newArray = new T[newSize];
    for (int i = 0; i < oldArray.Length; i++)
    {
        newArray[i] = oldArray[i];
    }
    return newArray;
}

void ExpandArrays()
{
    int newSize = todos.Length * GrowthFactor;
    todos = ExpandArray(todos, newSize);
    todos[todoCount] = parts[1];
    statuses[todoCount] = false;
    dates[todoCount] = DateTime.Now;
    todoCount++;
    Console.WriteLine($"(массив расширен до {newSize})");
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
bool TryGetIndex(string text, out int index)
{
    if (int.TryParse(text.Trim(), out int number) && number >= 1 && number <= todoCount)
    {
        index = number - 1;   // пользователь вводит с 1, внутри — с 0
        return true;
    }
    index = -1;
    Console.WriteLine("Ошибка. Неверный номер задачи.");
    return false;
}

void DoneTask(string args)
{
    if (!TryGetIndex(args, out int index)) return;
    statuses[index] = true;
    dates[index] = DateTime.Now;
    Console.WriteLine("Задача отмечена выполненной");
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
        case CommandDone: DoneTask(args); break;
        case CommandExit:
            Console.WriteLine("Завершение работы");
            return;
        default:
            Console.WriteLine("Неизвестная команда. Введите help.");
            break;
    }
}

