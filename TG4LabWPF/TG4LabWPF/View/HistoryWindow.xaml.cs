using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TG4LabWPF.Model.Attractions;

namespace TG4LabWPF.View
{
    public partial class HistoryWindow : Window
    {
        private readonly string historyPath;

        public HistoryWindow()
        {
            InitializeComponent();

            // Чтение пути из конфигурации
            var app = (App)Application.Current;
            historyPath = app.Configuration["HistoryFilePath"];

            History_Window.WindowState = WindowState.Maximized;
        }

        public async Task GetHistoryAsync()
        {
            try
            {
                // Проверяем, существует ли файл. Если нет — создаем пустой JSON массив
                if (!File.Exists(historyPath))
                {
                    await File.WriteAllTextAsync(historyPath, "[]");
                }

                // Чтение существующих данных из файла
                string json = await File.ReadAllTextAsync(historyPath);

                // Десериализуем данные как List<List<Route>>, где каждый сектор — это список маршрутов
                var existingRoutes = JsonConvert.DeserializeObject<List<List<Route>>>(json) ?? new List<List<Route>>();

                // Добавляем каждый сектор маршрутов в список history
                foreach (var sector in existingRoutes)
                {
                    // Создаем строку, объединяя все маршруты через запятую
                    string sectorString = string.Join(", ", sector.Select(r => $"{r.Name} ({r.Region})"));

                    // Добавляем строку в ListView
                    history.Items.Add(sectorString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnClearHistory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(historyPath))
                {
                    var historyAttractions = history.Items;

                    if (historyAttractions.Count > 0)
                    {
                        history.Items.Clear();
                    }

                    // Удаляем файл истории асинхронно
                    await Task.Run(() => File.Delete(historyPath));
                    Console.WriteLine("История успешно очищена.");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, выберите достопримечательности для удаления.",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnReturnPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
