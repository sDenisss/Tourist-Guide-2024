using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;

namespace TG.Commands.MenuCommands.PlanRoute
{
    public class AddSight
    {
        List<RouteAttractions> route = new List<RouteAttractions>();
        public List<RouteAttractions> AddSightM(AttractionList? db, List<Attraction>? attractions)
        {

            if (attractions == null || attractions.Count == 0)
            {
                Console.WriteLine("Нет доступных достопримечательностей.");
                return null;
            }

            while (true)
            {
                Console.WriteLine("1. Добавить достопримечательность");
                Console.WriteLine("2. Готово");
                string? choice = Console.ReadLine();

                if (choice == "1")
                {
                    // Вывод списка доступных достопримечательностей
                    for (int i = 0; i < attractions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {attractions[i].Name}");
                    }

                    Console.WriteLine("Выберите номер достопримечательности для добавления в маршрут:");
                    
                    // Чтение пользовательского ввода с проверкой на валидность
                    if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= attractions.Count)
                    {
                        // Индексы в списках начинаются с 0, а пользователь выбирает с 1, потому делаем корректировку
                        int index = selection - 1;
                        // Добавляем выбранную достопримечательность в маршрут
                        route.Add(new RouteAttractions { Id = attractions[index].Id, Name = attractions[index].Name });
                        Console.WriteLine($"{attractions[index].Name} добавлена в маршрут.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор! Пожалуйста, попробуйте снова.");
                    }
                }
                else if (choice == "2")
                {
                    foreach (var item in route)
                    {
                        Console.WriteLine(item.Name);
                    }
                    break;
                }
            }
            return route;
        }
    }
}