using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace TG.Users
{
    public class Auth : IEnterSystem
    {

        /// Асинхронно проверяет, совпадает ли введённый пароль с хранимым в базе.
        /// inputPassword Пароль, который ввёл пользователь
        /// storedSalt Сохранённая соль (строка)
        /// storedHash Сохранённый хэш пароля (строка)
        /// Возвращает True, если пароль совпадает, иначе False
        public async Task<bool> VerifyPasswordAsync(string inputPassword, string storedSalt, string storedHash)
        {
            // Асинхронно преобразуем соль из строки в массив байтов.
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            // Асинхронно хэшируем введённый пароль.
            string computedHash = await Task.Run(() =>
            {
                using var sha512 = SHA512.Create();
                byte[] computedHashBytes = sha512.ComputeHash(Combine(inputPassword, saltBytes));
                return Convert.ToBase64String(computedHashBytes);
            });

            // Сравниваем хэши.
            return computedHash == storedHash;
        }


        /// Склеивает пароль и соль в одно целое
        /// Возвращает Массив байтов с паролем и солью
        public byte[] Combine(string password, byte[] salt)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + salt.Length];
            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, combinedBytes, passwordBytes.Length, salt.Length);
            return combinedBytes;
        }

    }
}
