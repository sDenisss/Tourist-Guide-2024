using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TG.Attractions;
using TG.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TG.History
{
    public class HistoryActions
    {
        // Метод для регистрации маршрутов
        public static void Acts(WebApplication app, IConfiguration configuration)
        {
            // Получаем строку подключения из конфигурации
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            app.MapGet("/getHistory", async (int userId, UserDbContext db) =>
            {
                try
                {
                    var history = await db.UserHistories
                        .Where(h => h.UserId == userId)
                        .OrderByDescending(h => h.Date)
                        .Select(h => new { action = h.Action, date = h.Date })
                        .ToListAsync();

                    return Results.Ok(history);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = $"Ошибка: {ex.Message}" });
                }
            });

            app.MapPost("/saveHistory", async (HttpContext context, UserDbContext db) =>
            {
                try
                {
                    using var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();
                    var requestData = JsonConvert.DeserializeObject<SaveHistoryRequest>(content);

                    if (requestData == null || requestData.Route == null || !requestData.Route.Any())
                    {
                        return Results.BadRequest(new { message = "Некорректные данные." });
                    }

                    var places = string.Join(", ", requestData.Route.Select(p => $"{p.Name} ({p.Region})"));

                    var history = new UserHistory
                    {
                        UserId = requestData.UserId,
                        Action = places,
                        Date = DateTime.UtcNow
                    };

                    db.UserHistories?.Add(history);
                    await db.SaveChangesAsync();

                    return Results.Ok(new { message = "История успешно сохранена." });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { message = $"Ошибка: {ex.Message}" });
                }
            });

            app.MapPost("/clearHistory", async (HttpContext context) =>
            {
                try
                {
                    // Читаем тело запроса
                    using var reader = new StreamReader(context.Request.Body);
                    var content = await reader.ReadToEndAsync();

                    // Парсим JSON для получения userId
                    var data = JsonConvert.DeserializeObject<Dictionary<string, int>>(content);

                    if (data == null || !data.ContainsKey("userId") || data["userId"] <= 0)
                    {
                        return Results.BadRequest(new { message = "Некорректный userId." });
                    }

                    var userId = data["userId"];

                    // Устанавливаем соединение с SQLite
                    using var connection = new SqliteConnection(connectionString);
                    await connection.OpenAsync();

                    // Выполняем SQL-запрос
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM UserHistory WHERE UserId = @UserId;";
                    command.Parameters.AddWithValue("@UserId", userId);

                    var rowsAffected = await command.ExecuteNonQueryAsync();

                    // Возвращаем успешный результат
                    return Results.Ok(new { message = $"{rowsAffected} записи(ей) удалены." });
                }
                catch (Exception ex)
                {
                    // Возвращаем ошибку
                    return Results.BadRequest(new { message = $"Ошибка: {ex.Message}" });
                }
            });
        }
    }
}
