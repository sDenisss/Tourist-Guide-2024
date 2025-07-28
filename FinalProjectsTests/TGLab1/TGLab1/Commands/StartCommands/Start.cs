using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Commands;
using TG.Commands.MenuCommands;
using TG.Attractions;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace TG.Commands.StartCommands
{
    public class Start
    {
        public async Task StartProgramAsync()
        {
            try
            {
                AttractionList db = new AttractionList();
                db.CreateDatabase();

                bool isProcessing = false;
                List<Attraction> attractions = new List<Attraction>();
                List<RouteAttractions> route = new List<RouteAttractions>();

                var exit = new Exit();

                while (!isProcessing)
                {
                    Console.WriteLine("1. Поиск достопримечательностей по региону");
                    Console.WriteLine("2. Планирование маршрута");
                    Console.WriteLine("3. Просмотр истории рекомендованных путей");
                    Console.WriteLine("4. Очистить историю");
                    Console.WriteLine("5. Выйти");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            attractions = new FindSight().FindSightM(db);
                            break;
                        case "2":
                            route = new AddSight().AddSightM(attractions);
                            break;
                        case "3":
                            var history = await exit.saveLastSession.LoadAllHistoryAsync();
                            Console.WriteLine("\nИстория маршрутов:");
                            foreach (var session in history)
                            {
                                Console.WriteLine("------");
                                foreach (var r in session)
                                {
                                    Console.WriteLine($"Маршрут: {r.Name}");
                                }
                            }
                            break;
                        case "4":
                            await exit.saveLastSession.ClearHistoryAsync();
                            break;
                        case "5":
                            await exit.ExitProgram(route);
                            isProcessing = true;
                            break;
                        default:
                            Console.WriteLine("Некорректная команда. Попробуйте снова!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}