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