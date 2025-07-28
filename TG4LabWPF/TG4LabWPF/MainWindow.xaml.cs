using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TG4LabWPF.ViewModel;
using TG4LabWPF.View;
using TG4LabWPF.MVVM;
using System.IO;
using System.Windows.Navigation;
using TG4LabWPF.Model.Attractions;

namespace TG4LabWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel();
            DataContext = vm;
            //Main_Window.WindowStyle = WindowStyle.None;
            Main_Window.WindowState = WindowState.Maximized;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();

        }
        private void btnPlan_Click(object sender, RoutedEventArgs e)
        {
            PlanRouteWindow planRouteWindow = new PlanRouteWindow();
            planRouteWindow.Show();
            this.Close();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow historyWindow = new HistoryWindow();
            historyWindow.Show();
            historyWindow.GetHistoryAsync();
            this.Close();
        }
    }
}