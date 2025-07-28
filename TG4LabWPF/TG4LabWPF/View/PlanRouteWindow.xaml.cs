using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;
using TG4LabWPF.Model.Attractions;
using TG4LabWPF.MVVM;

using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TG4LabWPF.View
{
    public partial class PlanRouteWindow : Window
    {
        private readonly string historyPath;
        public readonly string databaseConnectionString;

        public PlanRouteWindow()
        {
            InitializeComponent();

            // Чтение конфигурации из файла
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Указываем текущую директорию
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            historyPath = configuration["HistoryFilePath"];
            databaseConnectionString = configuration.GetSection("Database")["ConnectionString"];

            Plan_Route_Window.WindowState = WindowState.Maximized;
        }

        private void btnReturnPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            InputRegion.Text = string.Empty;
        }

        private async void btnReady_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRoutes = route.Items
                    .Cast<dynamic>()
                    .Select(item => new Route
                    {
                        Name = item.Name,
                        Region = item.Region
                    })
                    .ToList();

                if (selectedRoutes == null || selectedRoutes.Count == 0)
                {
                    return;
                }

                var allHistory = await LoadAllHistoryAsync();
                allHistory.Add(selectedRoutes);

                string jsonString = JsonSerializer.Serialize(allHistory, new JsonSerializerOptions { WriteIndented = true });

                await File.WriteAllTextAsync(historyPath, jsonString);

                MessageBox.Show("Данные успешно добавлены!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Асинхронная загрузка всей истории из общего JSON-файла
        public async Task<List<List<Route>>> LoadAllHistoryAsync()
        {
            if (!File.Exists(historyPath))
            {
                return new List<List<Route>>(); // Если файла нет, возвращаем пустую историю
            }

            try
            {
                string jsonString = await File.ReadAllTextAsync(historyPath);
                var allHistory = JsonSerializer.Deserialize<List<List<Route>>>(jsonString);
                return allHistory ?? new List<List<Route>>(); // Возвращаем данные или пустую коллекцию
            }
            catch (Exception ex)
            {
                return new List<List<Route>>(); // Возвращаем пустую историю при ошибке
            }
        }

        private async void btnRemoveFromRoute_Click(object sender, RoutedEventArgs e)
        {
            var selectedAttractions = route.Items;

            if (selectedAttractions.Count > 0)
            {
                route.Items.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите достопримечательности для удаления.",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void btnAddInRoute_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var selectedAttractions = results.SelectedItems;

                    if (selectedAttractions.Count > 0)
                    {
                        foreach (var item in selectedAttractions)
                        {
                            route.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите достопримечательности для добавления.",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обработке запроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void btnSearchSearch_Click(object sender, RoutedEventArgs e)
        {
            string region = InputRegion.Text.Trim();

            string validationMessage = RegionValidator.ValidateRegion(region);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                MessageBox.Show(validationMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                try
                {
                    var attractions = await db.Attractions
                        .Where(h => h.Region.ToLower().Contains(region.ToLower()))
                        .Select(h => new { h.Name, h.Region })
                        .Take(50)
                        .ToListAsync(); // Используем асинхронный метод ToListAsync()

                    if (attractions.Any())
                    {
                        results.Items.Clear();
                        foreach (var item in attractions)
                        {
                            results.Items.Add(new { Name = item.Name, Region = item.Region });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обработке запроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        //public static async Task AddDataAsync(string inputText)
        //{
        //    using (var db = new SqliteConnection($"Data Source={databaseConnectionString}"))
        //    {
        //        await db.OpenAsync(); // Асинхронно открываем соединение

        //        var insertCommand = new SqliteCommand
        //        {
        //            Connection = db,
        //            CommandText = "INSERT INTO Attractions (Name) VALUES (@Entry);"
        //        };
        //        insertCommand.Parameters.AddWithValue("@Entry", inputText);

        //        await insertCommand.ExecuteNonQueryAsync(); // Асинхронное выполнение запроса
        //    }
        //}
    }
}
