using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG4LabWPF.Model.Attractions;

using Microsoft.Extensions.Configuration;

namespace TG4LabWPF.MVVM
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; } = null;

        private readonly string _path;

        public AppDbContext()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

            _path = config["Database:ConnectionString"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Attraction>(entity =>
        //    {
        //        entity.ToTable("Attractions");
        //        entity.HasKey(a => a.Id);
        //        entity.Property(a => a.Name).HasColumnType("TEXT");
        //        entity.Property(a => a.Region).HasColumnType("TEXT");
        //    });
        //}
    }
}
