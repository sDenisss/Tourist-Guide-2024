using System.ComponentModel.DataAnnotations.Schema;

namespace TG.Users
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? History { get; set; }
        // public string Salt{get; set;}

        public User() { }
    }
}
