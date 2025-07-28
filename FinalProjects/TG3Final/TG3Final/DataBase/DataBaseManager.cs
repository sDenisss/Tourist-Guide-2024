// using Microsoft.Data.Sqlite;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Options;

// namespace TG.DataBase
// {
//     public class DataBaseManager
//     {

//         private readonly string connectionString;
//         public DataBaseManager(IConfiguration configuration)
//         {
//             connectionString = configuration.GetConnectionString("DefaultConnection");
//         }
//         public void DataBaseM()
//         {
//             // string connectionString = "Data Source=mydatabase.db";
//             // Получаем строку подключения из файла конфигурации
            
//             // Открываем подключение к базе данных
//             using (var connection = new SqliteConnection(connectionString))
//             {
//                 connection.Open();
                
//                 // Создаем команду для выполнения SQL-запроса
//                 var command = connection.CreateCommand();
//                 // command.CommandText = @"
//                 //     CREATE TABLE IF NOT EXISTS Attractions (
//                 //         Id INTEGER PRIMARY KEY AUTOINCREMENT,
//                 //         Name TEXT NOT NULL,
//                 //         Region TEXT NOT NULL
//                 //     );
//                 // ";
//                 // command.ExecuteNonQuery();
                
//                 // // Вставляем данные
//                 // command.CommandText = @"
//                 //     INSERT INTO Attractions (Name, Region)
//                 //     VALUES ('Эйфелева башня', 'Париж'),
//                 //         ('Колизей', 'Рим'), ('Пантеон', 'Рим');
//                 // ";
//                 // command.Parameters.AddWithValue("$region", a);
//                 // command.ExecuteNonQuery();

//                 // // Delete duplicates
//                 // command.CommandText = @"
//                 //     DELETE FROM Attractions
//                 //     WHERE rowid NOT IN (
//                 //         SELECT MIN(rowid)
//                 //         FROM Attractions
//                 //         GROUP BY Name);
//                 // ";
//                 // command.ExecuteNonQuery();

//                 // Чтение данных
//                 command.CommandText = "SELECT Id, Name, Region FROM Attractions;";

//                 using (var reader = command.ExecuteReader())
//                 {
//                     while (reader.Read())
//                     {
//                         Console.WriteLine($"{reader.GetInt32(0)}: {reader.GetString(1)} - {reader.GetString(2)}");
//                     }
//                 }
//             }
//         }
//     }
// }