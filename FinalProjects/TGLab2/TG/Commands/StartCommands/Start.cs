using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Commands;
using TG.Commands.MenuCommands.Exit;
using TG.Attractions;
using TG.DataBase;
using TG.Users;
using System.Text.Json;
using TG.Commands.MenuCommands.FindSight;
using TG.Commands.MenuCommands.PlanRoute;
using TG.Commands.MenuCommands.HistoryRoute;
using Microsoft.EntityFrameworkCore;

namespace TG.Commands.StartCommands
{
    public class Start
    {
        public AttractionsContext tgcontext;

        public Start()
        {
            // Инициализируем контекст базы данных
            var options = new DbContextOptionsBuilder<AttractionsContext>()
                .UseSqlite("Data Source=mydatabase.db") // Указываем путь к вашей базе данных
                .Options;
            tgcontext = new AttractionsContext(options);
        }
        public async Task StartProgram()
        {
            try
            {
                // Инициализация базы данных
                DatabaseHelper db = new DatabaseHelper();
                // DatabaseUsers dbUsers = new DatabaseUsers();
                db.InitializeDatabaseAsync();
                // dbUsers.InitializeUsersDatabase();

                // Вход в систему
                // new EnterSystem().EnterSystemM();

                // Основной цикл программы
                bool isProcessing = false;
                List<Attraction> attractions = new List<Attraction>();
                List<RouteAttractions> route = new List<RouteAttractions>();
                var findSight = new FindSight();
                var addSight = new AddSight();
                var showHistory = new ShowHistory();
                var add = new AddInDataBase(tgcontext);
                var exit = new Exit();

                while (!isProcessing)
                {
                    Console.WriteLine("1. Поиск достопримечательностей по региону");
                    Console.WriteLine("2. Планирование маршрута");
                    Console.WriteLine("3. Просмотр истории рекомендованных путей");
                    Console.WriteLine("4. Очистить историю");
                    Console.WriteLine("5. Добавить в базу данных достопримечательность");
                    Console.WriteLine("6. Выйти");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            attractions = await findSight.FindSightMAsync(db);
                            break;
                        case "2":
                            route = await addSight.AddSightMAsync(attractions);
                            if (route.Count == 0)
                            {
                                Console.WriteLine("Маршрут не содержит достопримечательностей");
                            }

                            break;
                        case "3":
                            var history = await exit.saveLastSession.LoadAllHistoryAsync();
                            showHistory.GetHistory(history);
                            break;
                        case "4":
                            await exit.saveLastSession.ClearHistoryAsync();
                            break;
                        case "5":
                            await add.AddAsync();
                            break;
                        case "6":
                            foreach (var item in route)
                            {
                                Console.WriteLine(item.Name);
                            }
                            // await exit.saveLastSession.SaveLastSessionMAsync(route);
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
