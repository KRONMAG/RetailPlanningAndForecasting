using System.Linq;
using System.Windows;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting
{
    /// <summary>
    /// Средство показа диалоговых окон
    /// </summary>
    public sealed class DialogService : IDialogService
    {
        /// <summary>
        /// Показ диалогового окна с указанным заголовком и текстом
        /// </summary>
        /// <param name="title">Заголовок сообщения</param>
        /// <param name="message">Текст сообщения</param>
        public void MessageDialog(string title, string message) =>
            DialogManager.ShowMessageAsync
            (
                (MetroWindow)App.Current.Windows
                    .Cast<Window>()
                    .First(window => window is MetroWindow && window.IsActive),
                title,
                message
            );

        /// <summary>
        /// Показ диалогового окна открытия файла
        /// </summary>
        /// <param name="path">
        /// Путь к указанному файлу, если файл не выбран, параметр имеет значения null
        /// </param>
        /// <returns>Истина, если файл был выбран, иначе - ложь</returns>
        public bool OpenFileDialog(out string path)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                return true;
            }
            path = null;
            return false;
        }

        /// <summary>
        /// Показ диалогового окна сохранения файла
        /// </summary>
        /// <param name="path">
        /// Путь к указанному файлу, если файл не указан, параметр равен null
        /// </param>
        /// <returns>Истина, если файл указан, иначе - ложь</returns>
        public bool SaveFileDialog(out string path)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
            {
                path = dialog.FileName;
                return true;
            }
            path = null;
            return false;
        }
    }
}