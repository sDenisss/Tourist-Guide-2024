using Microsoft.Data.Sqlite;
using System.Net;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TG.Users;
using TG4LabWPF.Model.Attractions;
using TG4LabWPF.MVVM;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace TG4LabWPF.View
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private readonly string historyPath;
        private readonly string databaseConnectionString;

        public SearchWindow()
        {
            InitializeComponent();

            // Чтение конфигурации из файла
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Указываем текущую директорию
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            historyPath = configuration["HistoryFilePath"];
            databaseConnectionString = configuration.GetSection("Database")["ConnectionString"];

            Search_Window.WindowState = WindowState.Maximized;
        }

        private void btnReturnPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSearchClear_Click(object sender, RoutedEventArgs e)
        {
            // Очистка поля ввода и результатов
            InputRegion.Text = string.Empty;
        }

        private async void btnSearchSearch_Click(object sender, RoutedEventArgs e)
        {
            string region = InputRegion.Text.Trim();

            // Валидация данных
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
                    // Асинхронный запрос с использованием базы данных
                    var attractions = await db.Attractions
                        .Where(h => h.Region.ToLower().Contains(region.ToLower()))
                        .Select(h => new { h.Name, h.Region })
                        .Take(50) // Лимит на количество записей
                        .ToListAsync(); // Используем асинхронный метод ToListAsync()

                    if (attractions.Any())
                    {
                        results.Items.Clear();
                        foreach (var item in attractions)
                        {
                            results.Items.Add(new { Name = item.Name, Region = item.Region });
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет результатов по вашему запросу.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при обработке запроса.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
