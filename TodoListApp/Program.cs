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
const int TextPreviewLength = 30;
const int IndexWidth = 5;
const int TextColumnWidth = TextPreviewLength + 3;   // 30 символов + "..."
const int StatusWidth = 10;
const int DateWidth = 16;
const string CommandHelp = "help";
const string CommandProfile = "profile";
const string CommandAdd = "add";
const string CommandView = "view";
const string CommandExit = "exit";
const string CommandDone = "done";
const string CommandDelete = "delete";
const string CommandUpdate = "update";
const string CommandRead = "read";
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
    Console.WriteLine("done    - отметить задачу выполненной, формат: done <номер>");
    Console.WriteLine("delete  - удалить задачу, формат: delete <номер>");
    Console.WriteLine("update  - изменить задачу, формат: update <номер> \"новый текст\"");
}

void ShowProfile()
{
    Console.WriteLine($"{firstName} {lastName}, {birthYear}");
}

string ShortText(string text)
{
    text = text.Replace("\n", " ");
    return text.Length > TextPreviewLength
        ? text.Substring(0, TextPreviewLength) + "..."
        : text;
}

void ViewTasks(string commandArgs)
{
    var flags = ParseFlags(commandArgs);
    bool showAll = HasFlag(flags, "all", "a");
    bool showIndex = showAll || HasFlag(flags, "index", "i");
    bool showStatus = showAll || HasFlag(flags, "status", "s");
    bool showDate = showAll || HasFlag(flags, "update-date", "d");

    if (todoCount == 0)
    {
        Console.WriteLine("Список задач пуст");
        return;
    }

    string BuildRow(string index, string text, string status, string date)
    {
        string row = "";
        if (showIndex) row += index.PadRight(IndexWidth) + " | ";
        row += text.PadRight(TextColumnWidth);
        if (showStatus) row += " | " + status.PadRight(StatusWidth);
        if (showDate) row += " | " + date.PadRight(DateWidth);
        return row;
    }

    string header = BuildRow("Index", "Text", "Status", "Updated");
    Console.WriteLine(header);
    Console.WriteLine(new string('-', header.Length));

    for (int i = 0; i < todoCount; i++)
    {
        Console.WriteLine(BuildRow(
            (i + 1).ToString(),
            ShortText(todos[i]),
            statuses[i] ? DoneText : NotDoneText,
            dates[i].ToString("dd.MM.yyyy HH:mm")));
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
    statuses = ExpandArray(statuses, newSize);
    dates = ExpandArray(dates, newSize);
    Console.WriteLine($"(массив расширен до {newSize})");
}
void AddTask(string commandArgs)
{
    var flags = ParseFlags(commandArgs);
    string text;

    if (HasFlag(flags, "multiline", "m"))
    {
        text = ReadMultilineText();
    }
    else
    {
        string[] parts = commandArgs.Split('"');
        if (parts.Length < 3 || parts[1].Trim() == "")
        {
            Console.WriteLine("Ошибка. Формат: add \"текст задачи\" или add --multiline");
            return;
        }
        text = parts[1];
    }

    if (text.Trim() == "")
    {
        Console.WriteLine("Ошибка. Задача пустая");
        return;
    }

    if (todoCount == todos.Length) ExpandArrays();
    todos[todoCount] = text;
    statuses[todoCount] = false;
    dates[todoCount] = DateTime.Now;
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

void DoneTask(string commandArgs)
{
    if (!TryGetIndex(commandArgs, out int index)) return;
    statuses[index] = true;
    dates[index] = DateTime.Now;
    Console.WriteLine("Задача отмечена выполненной");
}
void DeleteTask(string commandArgs)
{
    if (!TryGetIndex(commandArgs, out int index)) return;

    // сдвигаем все следующие элементы всех массивов влево
    for (int i = index; i < todoCount - 1; i++)
    {
        todos[i] = todos[i + 1];
        statuses[i] = statuses[i + 1];
        dates[i] = dates[i + 1];
    }
    todoCount--;
    todos[todoCount] = "";
    statuses[todoCount] = false;
    dates[todoCount] = default;
    Console.WriteLine("Задача удалена");
}
void UpdateTask(string commandArgs)
{
    // формат: update <номер> "новый текст"
    string[] parts = commandArgs.Split('"');
    if (parts.Length < 3 || parts[1].Trim() == "")
    {
        Console.WriteLine("Ошибка. Формат: update <номер> \"новый текст\"");
        return;
    }
    if (!TryGetIndex(parts[0], out int index)) return;

    todos[index] = parts[1];
    dates[index] = DateTime.Now;
    Console.WriteLine("Задача обновлена");
}
void ReadTask(string commandArgs)
{
    if (!TryGetIndex(commandArgs, out int index)) return;

    Console.WriteLine($"Текст: {todos[index]}");
    Console.WriteLine($"Статус: {(statuses[index] ? "выполнена" : "не выполнена")}");
    Console.WriteLine($"Последнее изменение: {dates[index]:dd.MM.yyyy HH:mm}");
}

HashSet<string> ParseFlags(string commandArgs)
{
    var flags = new HashSet<string>();
    // флаги ищем только до первых кавычек, чтобы не принять текст задачи за флаг
    string flagsPart = commandArgs.Split('"')[0];
    foreach (string token in flagsPart.Split(' ', StringSplitOptions.RemoveEmptyEntries))
    {
        if (token.StartsWith("--"))
        {
            flags.Add(token.Substring(2).ToLower());
        }
        else if (token.StartsWith("-"))
        {
            foreach (char c in token.Substring(1))   // -is = -i и -s
            {
                flags.Add(char.ToLower(c).ToString());
            }
        }
    }
    return flags;
}

bool HasFlag(HashSet<string> flags, string longName, string shortName)
{
    return flags.Contains(longName) || flags.Contains(shortName);
}
string ReadMultilineText()
{
    var lines = new List<string>();
    Console.WriteLine("Вводите строки. Для завершения введите !end");
    while (true)
    {
        Console.Write("> ");
        string? line = Console.ReadLine();
        if (line == null || line.Trim() == "!end") break;
        if (line.StartsWith("> ")) line = line.Substring(2);   // если пользователь сам ввёл "> "
        lines.Add(line);
    }
    return string.Join("\n", lines);
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
    string commandArgs = input.Length > command.Length ? input.Substring(command.Length).Trim() : "";
        switch (command)
    {
        case CommandHelp:    ShowHelp(); break;
        case CommandProfile: ShowProfile(); break;
        case CommandAdd:     AddTask(commandArgs); break;
        case CommandView: ViewTasks(commandArgs); break;
        case CommandDone: DoneTask(commandArgs); break;
        case CommandDelete: DeleteTask(commandArgs); break;
        case CommandUpdate: UpdateTask(commandArgs); break;
        case CommandRead: ReadTask(commandArgs); break;
        case CommandExit:
            Console.WriteLine("Завершение работы");
            return;
        default:
            Console.WriteLine("Неизвестная команда. Введите help.");
            break;
    }
}

