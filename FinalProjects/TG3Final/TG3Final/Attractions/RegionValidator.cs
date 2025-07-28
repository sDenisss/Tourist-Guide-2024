using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Users;

namespace TG.Attractions
{
    class RegionValidator
    {
        public static IResult ValidateRegion(string reg)
        {
            // Проверка названия
            if (string.IsNullOrWhiteSpace(reg))
            {
                return Results.BadRequest(new { message = "Регион не должен быть пустым" });
            }
            if (reg.Length < 3 || reg.Length > 20)
            {
                return Results.BadRequest(new { message = "Регион должен быть длиной от 3 до 20 символов" });
            }

            return null; // Все в порядке
        }
    }
}
