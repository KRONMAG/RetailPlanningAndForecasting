using MahApps.Metro.Controls;
using CodeContracts;
using RetailPlanningAndForecasting.Presentation;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.UI.ModelInitialization
{
    /// <summary>
    /// Окно инициализации модели планирования товарооборота
    /// </summary>
    public sealed partial class MainWindow : MetroWindow, IView
    {
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="viewModel">
        /// Модель представления инициализации модели планирования товарооборота
        /// </param>
        public MainWindow(ModelInitializationViewModel viewModel)
        {
            Requires.NotNull(viewModel, nameof(viewModel));

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}