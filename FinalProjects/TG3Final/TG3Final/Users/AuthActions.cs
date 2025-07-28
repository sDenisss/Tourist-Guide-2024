using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TG.DataBase;

namespace TG.Users
{
    public class AuthActions
    {
        public static void Acts(WebApplication app)
        {
            app.MapPost("/login", async (UserDbContext db, User loginUser, CancellationToken cancellationToken) =>
            {

                await Task.Delay(6000, cancellationToken);
                // Валидация данных пользователя
                var validationResult = UserValidator.ValidateUser(loginUser);
                if (validationResult != null)
                {
                    return validationResult; // Возвращаем ошибку, если проверка не прошла
                }

                // Проверяем, существует ли пользователь с таким именем
                var user = await db.User.FirstOrDefaultAsync(u => u.Username == loginUser.Username, cancellationToken);
                if (user == null)
                {
                    // Если пользователь не найден
                    return Results.BadRequest(new { message = "Неверное имя или пароль" });
                }

                // Проверяем пароль
                var auth = new Auth();
                bool isValidPassword = await auth.VerifyPasswordAsync(loginUser.Password, user.Salt, user.Password);

                if (!isValidPassword)
                {
                    // Если пароль неверный
                    return Results.BadRequest(new { message = "Неверное имя или пароль" });
                }

                // Успешный вход
                return Results.Ok(new { message = "Успешный вход", userId = user.Id });
            });


            // Маппинг маршрута регистрации
            app.MapPost("/registry", async (UserDbContext db, User regUser) =>
            {
                // Валидация данных пользователя
                var validationResult = UserValidator.ValidateUser(regUser);
                if (validationResult != null)
                {
                    return validationResult; // Возвращаем ошибку, если проверка не прошла
                }

                // Проверяем, есть ли пользователь с таким именем или почтой
                var existingUser = await db.User
                    .FirstOrDefaultAsync(u => u.Username == regUser.Username || u.Email == regUser.Email);

                if (existingUser != null)
                {
                    return Results.BadRequest(new { message = "Пользователь с таким именем или почтой уже существует." });
                }

                // Создаем экземпляр класса Registry и хэшируем пароль
                var registry = new Users.Registry();
                var passwordResult = await registry.HashPasswordAsync(regUser.Password);

                // Устанавливаем хэш и соль для пользователя
                regUser.Password = passwordResult.Hash;
                regUser.Salt = passwordResult.Salt;

                // Добавляем нового пользователя в базу данных
                db.User.Add(regUser);
                await db.SaveChangesAsync();

                // Успешная регистрация
                return Results.Ok(new { message = "Успешная регистрация", userId = regUser.Id });
            });


            app.MapPost("/getEmail", (UserDbContext db, User loginUser) => 
            {
                var user = db.User.FirstOrDefault(u => u.Username == loginUser.Username);

                if (user == null)
                {
                    return Results.BadRequest(new { message = "Неверный логин или пароль" });
                }

                return Results.Ok(new { email = user.Email });
            });

            app.MapPost("/getId", (UserDbContext db, [FromBody] User loginUser) => 
            {
                var user = db.User.FirstOrDefault(u => u.Username == loginUser.Username);

                if (user == null)
                {
                    return Results.BadRequest(new { message = "Неверный логин" });
                }

                return Results.Ok(new { id = user.Id });
            });

        }
    }
}