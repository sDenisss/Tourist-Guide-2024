using Microsoft.Data.Sqlite;
using TG.Attractions;

namespace TG.DataBase
{
    public class DatabaseHelper
    {
        private readonly string connectionString;

        // Конструктор принимает IConfiguration и извлекает строку подключения
        public DatabaseHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        

        public void InitializeDatabase()
        {
            Console.WriteLine("Initializing Attractions table...");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS Attractions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT,
                        Region TEXT
                    );
                ";
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Attractions table initialized.");
        }

        public void InitializeDatabaseUserHistory()
        {
            Console.WriteLine("Initializing UserHistory table...");
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS UserHistory (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER,
                        Action TEXT,
                        Date DATETIME DEFAULT CURRENT_TIMESTAMP
                    );
                ";
                command.ExecuteNonQuery();
            }
            Console.WriteLine("UserHistory table initialized.");
        }
        // public void AddAttraction(Attraction attraction)
        // {
        //     using (var connection = new SqliteConnection(_connectionString))
        //     {
        //         connection.Open();
        //         var command = connection.CreateCommand();
        //         command.CommandText =
        //         @"
        //             INSERT INTO Attractions (Name, Region)
        //             VALUES ($name, $region);
        //         ";
        //         command.Parameters.AddWithValue("$name", attraction.Name);
        //         command.Parameters.AddWithValue("$region", attraction.Region);
        //         command.ExecuteNonQuery();
        //     }
        // }

        // public List<Attraction> GetAttractionsByRegion(string region)
        // {
        //     var attractions = new List<Attraction>();
        //     using (var connection = new SqliteConnection(_connectionString))
        //     {
        //         connection.Open();
        //         var command = connection.CreateCommand();
        //         command.CommandText =
        //         @"
        //             SELECT Id, Name, Region
        //             FROM Attractions
        //             WHERE Region = $region;
        //         ";
        //         command.Parameters.AddWithValue("$region", region);
        //         using (var reader = command.ExecuteReader())
        //         {
        //             while (reader.Read())
        //             {
        //                 attractions.Add(new Attraction
        //                 {
        //                     Id = reader.GetInt32(0),
        //                     Name = reader.GetString(1),
        //                     Region = reader.GetString(2)
        //                 });
        //             }
        //         }
        //     }
        //     return attractions;
        // }

        // internal void UpdateAttraction(Attraction updatedAttraction)
        // {
        //     throw new NotImplementedException();
        // }

        // internal void DeleteAttraction(int id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}