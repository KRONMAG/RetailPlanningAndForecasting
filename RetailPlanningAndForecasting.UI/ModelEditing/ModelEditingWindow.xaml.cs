using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace RetailPlanningAndForecasting.UI.ModelEditing
{
    /// <summary>
    /// Окно редактирования модели товарооборота
    /// </summary>
    public partial class ModelEditingWindow : MetroWindow
    {
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public ModelEditingWindow()
        {
            InitializeComponent();
        }

        private async void Error(object sender, ValidationErrorEventArgs e)
        {
            await this.ShowMessageAsync("Ошибка", e.Error.ErrorContent.ToString());
        }
    }
}