using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.Attractions
{
    // Класс для обработки входных данных
    public class SaveHistoryRequest
    {
        public int UserId { get; set; }
        public List<Attraction> Route { get; set; }
    }

}