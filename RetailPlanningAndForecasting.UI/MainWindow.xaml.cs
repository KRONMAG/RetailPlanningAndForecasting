using System.Windows;
using MahApps.Metro.Controls;

namespace RetailPlanningAndForecasting.UI
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateModelClick(object sender, RoutedEventArgs e) =>
            new CreationModelWindow().Show();
    }
}
