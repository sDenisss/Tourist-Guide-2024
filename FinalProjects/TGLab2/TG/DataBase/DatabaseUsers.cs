// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.Data.Sqlite;
// using TG.Users;

// namespace TG.DataBase
// {
//     public class DatabaseUsers
//     {
//         private string _connectionString = "Data Source=mydatabase.db";

//         public void InitializeUsersDatabase()
//         {
//             using (var connection = new SqliteConnection(_connectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText =
//                 @"
//                     CREATE TABLE IF NOT EXISTS Users (
//                         Id INTEGER PRIMARY KEY AUTOINCREMENT,
//                         Username TEXT NOT NULL,
//                         Email TEXT NOT NULL UNIQUE,
//                         Password TEXT NOT NULL,
//                         Created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
//                         Salt TEXT,
//                         History TEXT
//                     );
//                 ";
//                 command.ExecuteNonQuery();
//             }
//         }

//         public void AddUser(User user)
//         {
//             using (var connection = new SqliteConnection(_connectionString))
//             {
//                 connection.Open();
//                 var command = connection.CreateCommand();
//                 command.CommandText =
//                 @"
//                     INSERT INTO Users (Username, Email, Password)
//                     VALUES ($username, $email, $password);
//                 ";
//                 command.Parameters.AddWithValue("$username", user.Username);
//                 command.Parameters.AddWithValue("$email", user.Email);
//                 command.Parameters.AddWithValue("$password", user.Password);
//                 command.ExecuteNonQuery();
//             }
//         }


//     }
// }