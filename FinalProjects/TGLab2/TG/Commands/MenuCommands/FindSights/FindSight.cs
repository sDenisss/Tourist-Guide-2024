using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TG.Attractions;
using TG.DataBase;


namespace TG.Commands.MenuCommands.FindSight
{
    public class FindSight
    {
        public async Task<List<Attraction>> FindSightMAsync(IData db)
        {
            Console.WriteLine("Enter region:");
            string? region = Console.ReadLine()?.Trim();

            var attractions = await db.GetAttractionsByRegionAsync(region);

            if (attractions == null || !attractions.Any())
            {
                Console.WriteLine("No attractions found in the specified region.");
                return new List<Attraction>();
            }

            Console.WriteLine("Attractions found:");
            foreach (var attraction in attractions)
            {
                Console.WriteLine($"- {attraction.Name}");
            }

            return attractions;
        }


    }
}
