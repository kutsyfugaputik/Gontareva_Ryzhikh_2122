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
        case "exit":
            Console.WriteLine("Завершение работы");
            return;

        default:
            Console.WriteLine("Неизвестная команда. Введите help.");
            break;
    }
}
