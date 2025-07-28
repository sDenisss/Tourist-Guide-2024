using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;

namespace TG.Commands.MenuCommands.HistoryRoute
{
    public class ShowHistory
    {
        public void GetHistory(List<RouteAttractions> route)
        {
            System.Console.WriteLine("История:");
            foreach (var item in route)
            {
                Console.WriteLine(item.Name);
            }
            System.Console.WriteLine("");
        }
    }
}