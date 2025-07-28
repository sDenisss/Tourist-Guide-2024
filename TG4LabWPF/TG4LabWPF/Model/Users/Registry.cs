using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace TG.Users
{
    public class Registry : IEnterSystem
    {
        /// Асинхронно хеширует пароль и генерирует соль.
        /// Возвращает Хэшированный пароль (строка)
        public async Task<PasswordHashResult> HashPasswordAsync(string password)
        {
            return await Task.Run(() =>
            {
                // Генерация соли
                byte[] saltBytes = RandomNumberGenerator.GetBytes(128 / 8);
                string salt = Convert.ToBase64String(saltBytes);

                // Хеширование пароля
                using var sha512 = SHA512.Create();
                byte[] hashBytes = sha512.ComputeHash(Combine(password, saltBytes));
                string hash = Convert.ToBase64String(hashBytes);

                // Возвращаем хэш и соль
                return new PasswordHashResult { Hash = hash, Salt = salt };
            });
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
