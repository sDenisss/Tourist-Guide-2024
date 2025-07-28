using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.Users;

namespace TG.Users
{
    // Класс для валидации пользователя
public static class UserValidator
{
    public static IResult ValidateUser(User regUser)
    {
        // Проверка имени пользователя
        if (string.IsNullOrWhiteSpace(regUser.Username))
        {
            return Results.BadRequest(new { message = "Имя пользователя не должно быть пустым" });
        }
        if (regUser.Username.Length < 3 || regUser.Username.Length > 20)
        {
            return Results.BadRequest(new { message = "Имя пользователя должно быть длиной от 3 до 20 символов" });
        }

        // Проверка пароля
        if (string.IsNullOrWhiteSpace(regUser.Password))
        {
            return Results.BadRequest(new { message = "Пароль не должен быть пустым" });
        }
        if (regUser.Password.Length < 3 || regUser.Password.Length > 100)
        {
            return Results.BadRequest(new { message = "Пароль должен быть длиной от 3 до 100 символов" });
        }
        return null; // Все в порядке
    }
}
}