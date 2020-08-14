using System.Windows;
using MahApps.Metro.Controls;
using RetailPlanningAndForecasting.UI.ModelEditing;

namespace RetailPlanningAndForecasting.UI.ModelInitialization
{
    /// <summary>
    /// Окно инициализации модели планирования товарооборота
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события нажатия кнопки создания модели: вызов окна редактирования ее показателей
        /// </summary>
        /// <param name="sender">Объект - источник события</param>
        /// <param name="e">Параметры собы</param>
        private void CreateModelClick(object sender, RoutedEventArgs e) =>
            new ModelEditingWindow().Show();
    }
}