using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using TG.Users;

namespace TG4LabWPF.Model.Attractions
{
    class RegionValidator
    {
        public static string ValidateRegion(string reg)
        {
            // Проверка названия
            if (string.IsNullOrWhiteSpace(reg))
            {
                return "Название не должно быть пустым";
            }
            if (reg.Length < 3 || reg.Length > 20)
            {
                return "Название должно быть длиной от 3 до 20 символов";
            }

            return null; // Все в порядке
        }
    }
}
