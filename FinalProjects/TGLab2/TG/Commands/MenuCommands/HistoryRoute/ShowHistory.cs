using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;
using TG.DataBase;

namespace TG.Commands.MenuCommands.HistoryRoute
{
    public class ShowHistory
    {
        public async void GetHistory(List<List<RouteAttractions>> history)
        {
            Console.WriteLine("\nИстория маршрутов:");
            foreach (var session in history)
            {
                Console.WriteLine("------");
                foreach (var r in session)
                {
                    Console.WriteLine($"Маршрут: {r.Name}");
                }
            }
        }
    }
}