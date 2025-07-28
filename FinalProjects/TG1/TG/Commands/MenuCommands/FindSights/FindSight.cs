using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TG.Attractions;

namespace TG.Commands.MenuCommands.FindSight
{
    public class FindSight
    {
        public List<Attraction> FindSightM(AttractionList? db)
        {
            Console.WriteLine("Введите регион:");
            string? region = Console.ReadLine();

            List<Attraction> attractions = db.GetAttractionsByRegion(region);

            if (region == "")
            {
                Console.WriteLine("Unknown region name!\n try again");
            } 

            for (int i = 0; i < attractions.Count; i++)
            {
                Console.WriteLine($"{i+1} - {attractions[i].Name}");
            }
            return attractions;
        }
    }
}