using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;

namespace TG.Commands.MenuCommands
{
    public class AddSight
    {
        private List<RouteAttractions> route = new List<RouteAttractions>();
        public static string? choice{get;set;}
        public List<RouteAttractions> AddSightM(List<Attraction>? attractions, Queue<string>? simulatedInputs = null)
        {
            if (attractions == null || attractions.Count == 0)
            {
                Console.WriteLine("Нет доступных достопримечательностей.");
                return new List<RouteAttractions>();
            }

            while (true)
            {
                Console.WriteLine("1. Добавить достопримечательность");
                Console.WriteLine("2. Готово");

                string? choice = simulatedInputs?.Dequeue() ?? Console.ReadLine();

                if (choice == "1")
                {
                    // Вывод списка доступных достопримечательностей
                    for (int i = 0; i < attractions.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {attractions[i].Name}");
                    }

                    Console.WriteLine("Выберите номер достопримечательности для добавления в маршрут:");
                    string? input = simulatedInputs?.Dequeue() ?? Console.ReadLine();

                    if (!string.IsNullOrEmpty(input) && input.All(char.IsDigit))
                    {
                        int selection = int.Parse(input);

                        if (selection > 0 && selection <= attractions.Count)
                        {
                            int index = selection - 1;
                            route.Add(new RouteAttractions { Id = attractions[index].Id, Name = attractions[index].Name });
                            Console.WriteLine($"{attractions[index].Name} добавлен в маршрут.");
                        }
                        else
                        {
                            Console.WriteLine("Неверный выбор! Выберите число в пределах доступных достопримечательностей.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод! Пожалуйста, введите число.");
                    }
                }
                else if (choice == "2")
                {
                    foreach (var item in route)
                    {
                        Console.WriteLine($"Id: {item.Id}, Name: {item.Name}");
                    }
                    break;
                }
            }
            return route;
        }

        // public List<RouteAttractions> AddSightM(List<Attraction>? attractions, string? choice = null, string? input = null)
        // {

        //     if (attractions == null || attractions.Count == 0)
        //     {
        //         Console.WriteLine("Нет доступных достопримечательностей.");
        //         return null;
        //     }

        //     while (true)
        //     {
        //         Console.WriteLine("1. Добавить достопримечательность");
        //         Console.WriteLine("2. Готово");
        //         // choice = Console.ReadLine();

        //         if (choice == "1")
        //         {
        //             // Вывод списка доступных достопримечательностей
        //             for (int i = 0; i < attractions.Count; i++)
        //             {
        //                 Console.WriteLine($"{i + 1} - {attractions[i].Name}");
        //             }

        //             Console.WriteLine("Выберите номер достопримечательности для добавления в маршрут:");

        //             // input = Console.ReadLine(); // Read user input

        //             // Check if the input is a valid integer
        //             if (!string.IsNullOrEmpty(input) && input.All(char.IsDigit))
        //             {
        //                 int selection = int.Parse(input); // Parse the input into an integer

        //                 if (selection > 0 && selection <= attractions.Count)
        //                 {
        //                     // Индексы в списках начинаются с 0, а пользователь выбирает с 1, потому делаем корректировку
        //                     int index = selection - 1;
        //                     // Добавляем выбранную достопримечательность в маршрут
        //                     route.Add(new RouteAttractions { Id = attractions[index].Id, Name = attractions[index].Name });
        //                     Console.WriteLine($"{attractions[index].Name} добавлен в маршрут.");
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine("Неверный выбор! Выберите число в пределах доступных достопримечательностей.");
        //                 }
        //             }
        //             else
        //             {
        //                 Console.WriteLine("Неверный ввод! Пожалуйста, введите число.");
        //             }

        //         }
        //         else if (choice == "2")
        //         {
        //             // foreach (var item in route)
        //             // {
        //             //     Console.WriteLine(item.Name);
        //             // }
        //             foreach (var item in route)
        //             {
        //                 Console.WriteLine($"Id: {item.Id}, Name: {item.Name}");
        //             }

        //             break;
        //         }
        //     }
        //     return route;
        // }
    }
}