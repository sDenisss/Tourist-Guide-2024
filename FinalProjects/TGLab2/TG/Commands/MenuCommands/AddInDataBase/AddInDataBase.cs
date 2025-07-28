using TG.Attractions;
using TG.DataBase;

public class AddInDataBase
{
    private readonly AttractionsContext _context;

    public AddInDataBase(AttractionsContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAsync()
    {
        if (_context == null)
        {
            Console.WriteLine("Контекст базы данных не инициализирован.");
            return;
        }

        while (true)
        {
            Console.WriteLine("1. Добавить достопримечательность");
            Console.WriteLine("2. Готово");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите название достопримечательности:");
                    string? name = Console.ReadLine();

                    Console.WriteLine("Введите регион достопримечательности:");
                    string? region = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(region))
                    {
                        Console.WriteLine("Название и регион не могут быть пустыми.");
                        continue;
                    }

                    // Создаем объект достопримечательности
                    var newAttraction = new Attraction(0, name, region);

                    // Добавляем объект в бд
                    await _context.Attractions.AddAsync(newAttraction);
                    await _context.SaveChangesAsync();

                    Console.WriteLine($"Достопримечательность '{name}' в регионе '{region}' успешно добавлена!");
                    break;

                case "2":
                    Console.WriteLine("Выход из режима добавления.");
                    return;

                default:
                    Console.WriteLine("Некорректный выбор, попробуйте снова.");
                    break;
            }
        }
    }
}
