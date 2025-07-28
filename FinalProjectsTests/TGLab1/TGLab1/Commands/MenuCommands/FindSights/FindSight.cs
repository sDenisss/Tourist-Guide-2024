using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TG.Attractions;

namespace TG.Commands.MenuCommands
{
    public class FindSight
    {
        public static string? region{get; set;}
        public List<Attraction> FindSightM(AttractionList? db, string? region = null)
        {
            System.Console.WriteLine("Введите регион:");
            // region = System.Console.ReadLine();

            // Проверяем, что введенный регион не является null или пустой строкой
            if (string.IsNullOrWhiteSpace(region))
            {
                Console.WriteLine("Unknown region name! Try again.");
                return new List<Attraction>(); // Возвращаем пустой список, если регион некорректен
            }

            // Получаем список достопримечательностей по региону
            List<Attraction> attractions = db.GetAttractionsByRegion(region);

            // Выводим список достопримечательностей
            for (int i = 0; i < attractions.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {attractions[i].Name}");
            }
            return attractions;
        }
    }
}
