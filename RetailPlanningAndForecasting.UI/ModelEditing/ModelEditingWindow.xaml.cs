using MahApps.Metro.Controls;
using CodeContracts;
using RetailPlanningAndForecasting.Presentation;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.UI.ModelEditing
{
    /// <summary>
    /// Окно редактирования модели товарооборота
    /// </summary>
    public sealed partial class ModelEditingWindow : MetroWindow, IView
    {
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="viewModel">
        /// Модель представления редактирования модели планирования товарооборота
        /// </param>
        public ModelEditingWindow(ModelEditingViewModel viewModel)
        {
            Requires.NotNull(viewModel, nameof(viewModel));

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}