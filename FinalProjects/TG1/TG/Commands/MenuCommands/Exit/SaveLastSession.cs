using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TG.Attractions;


namespace TG.Commands.MenuCommands.Exit
{
    public class SaveLastSession
    {
        private readonly string _historyFilePath;

        public SaveLastSession()
        {
            var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "data");
            Directory.CreateDirectory(dataFolder); // Создаём папку, если она отсутствует
            _historyFilePath = Path.Combine(dataFolder, "history.json");
        }

        // Асинхронное сохранение последней сессии в общий JSON-файл
        public async Task SaveLastSessionMAsync(List<RouteAttractions> route)
        {
            var allHistory = await LoadAllHistoryAsync(); // Загружаем существующую историю

            allHistory.Add(route); // Добавляем новую сессию

            // Сериализация истории в JSON
            string jsonString = JsonSerializer.Serialize(allHistory, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_historyFilePath, jsonString);

            Console.WriteLine("Последняя сессия добавлена в историю.");
        }

        // Асинхронная загрузка всей истории из общего JSON-файла
        public async Task<List<List<RouteAttractions>>> LoadAllHistoryAsync()
        {
            if (!File.Exists(_historyFilePath))
            {
                return new List<List<RouteAttractions>>(); // Если файла нет, возвращаем пустую историю
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(_historyFilePath);
                var allHistory = JsonSerializer.Deserialize<List<List<RouteAttractions>>>(jsonString);
                return allHistory ?? new List<List<RouteAttractions>>(); // Возвращаем данные или пустую коллекцию
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке истории: {ex.Message}");
                return new List<List<RouteAttractions>>(); // Возвращаем пустую историю при ошибке
            }
        }

        // Асинхронная очистка всей истории
        public async Task ClearHistoryAsync()
        {
            try
            {
                if (File.Exists(_historyFilePath))
                {
                    File.Delete(_historyFilePath); // Удаляем файл истории
                    await Task.CompletedTask; // Для соответствия async
                    Console.WriteLine("История успешно очищена.");
                }
                else
                {
                    Console.WriteLine("Файл истории отсутствует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при очистке истории: {ex.Message}");
            }
        }
    }
}