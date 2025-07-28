using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TG.Attractions;
using TG.DataBase;

namespace TG.Commands.MenuCommands.PlanRoute
{
    public class AddSight
    {
        public async Task<List<RouteAttractions>> AddSightMAsync(List<Attraction>? attractions, Queue<string>? simulatedInputs = null)
        {
            var route = new List<RouteAttractions>();

            if (attractions == null || attractions.Count == 0)
            {
                Console.WriteLine("Нет доступных достопримечательностей.");
                return route;
            }

            while (true)
            {
                Console.WriteLine("1. Добавить достопримечательность");
                Console.WriteLine("2. Готово");

                string? choice = simulatedInputs?.Dequeue() ?? Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Выберите номер достопримечательности для добавления в маршрут:");
                    for (int i = 0; i < attractions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {attractions[i].Name}");
                    }

                    string? input = simulatedInputs?.Dequeue() ?? Console.ReadLine();
                    if (int.TryParse(input, out int selection) && selection > 0 && selection <= attractions.Count)
                    {
                        var selectedAttraction = attractions[selection - 1];
                        if (!route.Any(r => r.Id == selectedAttraction.Id))
                        {
                            route.Add(new RouteAttractions { Id = selectedAttraction.Id, Name = selectedAttraction.Name });
                            Console.WriteLine($"{selectedAttraction.Name} добавлена в маршрут.");
                        }
                        else
                        {
                            Console.WriteLine($"{selectedAttraction.Name} уже добавлена.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор! Попробуйте снова.");
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Маршрут сформирован:");
                    foreach (var item in route)
                    {
                        Console.WriteLine($"Id: {item.Id}, Name: {item.Name}");
                    }
                    break;
                }
            }

            return route;
        }

    }
}
