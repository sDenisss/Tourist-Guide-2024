using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TG.History
{
    [Table("UserHistory")]
    public class UserHistory
    {
        public int Id { get; set; } // Идентификатор записи (ключ)
        public int UserId { get; set; }  // Идентификатор пользователя
        public string Action { get; set; }  // Описание действия пользователя
        public DateTime Date { get; set; }  // Дата и время совершения действия
    }

}