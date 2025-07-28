using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.DataBase;

namespace TG.Attractions
{
    public class AttractionsActions
    {
        public static void Acts(WebApplication app)
        {
            app.MapGet("/attractions", async (AttractionDbContext db, string? region) =>
            {
                IQueryable<Attraction>? attractions = db.Attraction;

                // Валидация данных
                var validationMessage = RegionValidator.ValidateRegion(region);
                if (validationMessage != null)
                {
                    return Results.BadRequest(new { message = validationMessage });
                }

                // Фильтрация по региону, если параметр указан
                if (!string.IsNullOrEmpty(region))
                {
                    attractions = attractions.Where(a => a.Region.Contains(region));
                }

                return Results.Ok(attractions);
            });
        }
    }
}