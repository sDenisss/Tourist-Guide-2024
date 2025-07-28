using Microsoft.EntityFrameworkCore;
using TG.Attractions;

namespace TG.DataBase
{
    public class TouristGuideContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; }

        private readonly string _connectionString = "Data Source=mydatabase.db";

        //Настройка провайдера базы данных и подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString); //Настраивает, какой провайдер базы данных использовать
        }

        //Настройка схемы базы данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attraction>(entity =>
            {
                entity.HasKey(a => a.Id); // Указание первичного ключа
                entity.Property(a => a.Name)
                    .IsRequired()
                    .HasMaxLength(20); // Ограничение длины строки
                entity.Property(a => a.Region)
                    .IsRequired();
            });
        }

    }
}
