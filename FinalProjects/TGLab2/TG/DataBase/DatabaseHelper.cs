using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using TG.Attractions;
using Microsoft.EntityFrameworkCore;

namespace TG.DataBase
{
    public class DatabaseHelper : IData
    {
        // private readonly string _connectionString = "Data Source=mydatabase.db";

        public async Task InitializeDatabaseAsync()
        {
            using (var context = new TouristGuideContext())
            {
                // Асинхронно создаем базу данных, если она еще не существует
                await context.Database.EnsureCreatedAsync();
            }
        }

        public async Task<List<Attraction>> GetAttractionsByRegionAsync(string region)
        {
            using (var context = new TouristGuideContext())
            {
                // Асинхронно выполняем запрос к базе данных
                return await context.Attractions
                    .Where(a => a.Region == region)
                    .ToListAsync();
            }
        }
    }
}
