using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TG.History;
using TG.Users;


namespace TG.DataBase
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options){}

        public DbSet<User>? User { get; set; }
        public DbSet<UserHistory>? UserHistories { get; set; }
    }
}


// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using TG.History;
// using TG.Users;


// namespace TG.DataBase
// {
//     public class UserDbContext : DbContext
//     {
//         private readonly IConfiguration _configuration;

//         public UserDbContext(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         protected override void OnConfiguring(DbContextOptionsBuilder options)
//         {
//             var connectionString = _configuration.GetConnectionString("DefaultConnection");
//             options.UseSqlite(connectionString);

//             // Ensure the database and tables are created
//             using (var context = new UserDbContext(_configuration))
//             {
//                 context.Database.EnsureCreated();  // This will create the table if it doesn't exist
//             }
//         }

//         public DbSet<User>? User { get; set; }
//         public DbSet<UserHistory>? UserHistories { get; set; }
//     }
// }
