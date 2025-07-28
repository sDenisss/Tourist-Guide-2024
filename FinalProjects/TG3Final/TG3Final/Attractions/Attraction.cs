using System.ComponentModel.DataAnnotations.Schema;

namespace TG.Attractions
{
    [Table("Attractions")]
    public class Attraction
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Region { get; set; }

    }

}
