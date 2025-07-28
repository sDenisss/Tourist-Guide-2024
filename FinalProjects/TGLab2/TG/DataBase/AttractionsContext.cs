using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TG.Attractions;

namespace TG.DataBase
{
    public class AttractionsContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; }

        public AttractionsContext(DbContextOptions<AttractionsContext> options)
            : base(options)
        {
        }

        //Настройка схемы базы данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attraction>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Attraction>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Attraction>()
                .Property(a => a.Region)
                .IsRequired();
        }
    }
}
