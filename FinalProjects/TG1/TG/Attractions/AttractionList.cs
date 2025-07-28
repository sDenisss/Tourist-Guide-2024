using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;

namespace TG.Attractions
{
    public class AttractionList
    {
        private List<Attraction> Attractions = new List<Attraction>();

        // Метод для создания базы данных достопримечательностей
        public void CreateDatabase()
        {
            Add(new Attraction(Attractions.Count, "Louvre", "Paris"));
            Add(new Attraction(Attractions.Count, "Coliseum", "Rome"));
            Add(new Attraction(Attractions.Count, "Eiffel Tower", "Paris"));
        }

        // Упрощённый метод добавления достопримечательности
        public void Add(Attraction attraction)
        {
            attraction.Id = Attractions.Count; // Назначаем ID на основе текущего размера списка
            Attractions.Add(attraction);
        }

        // Получение списка достопримечательностей по региону
        public List<Attraction> GetAttractionsByRegion(string region)
        {
            return Attractions
                .Where(attraction => attraction.Region?.Equals(region, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
        }
    }
}
