using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Attractions;

namespace TG.DataBase
{
    public interface IData
    {
        public Task<List<Attraction>> GetAttractionsByRegionAsync(string region);
    }
}