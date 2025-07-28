using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TG.Attractions
{
    public class Attraction
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }

        public Attraction(int id, string? name, string? region)
        {
            Id = id;
            Name = name;
            Region = region;
        }

    }

}
