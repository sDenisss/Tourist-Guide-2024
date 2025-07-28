using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TG.Attractions;

namespace TG.DataBase
{
    public class AttractionDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public AttractionDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // Получаем строку подключения из файла конфигурации
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseSqlite(connectionString);
        }


        public DbSet<Attraction>? Attraction { get; set; }
    }
}
