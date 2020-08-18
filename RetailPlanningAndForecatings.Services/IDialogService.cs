namespace RetailPlanningAndForecasting.Services
{
    /// <summary>
    /// Описание функционала показа диалоговых окон
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Показ диалогового окна с указанным заголовком и текстом
        /// </summary>
        /// <param name="title">Заголовок сообщения</param>
        /// <param name="message">Текст сообщения</param>
        void MessageDialog(string title, string message);

        /// <summary>
        /// Показ диалогового окна открытия файла
        /// </summary>
        /// <param name="path">
        /// Путь к указанному файлу, если файл не выбран, параметр имеет значения null
        /// </param>
        /// <returns>Истина, если файл был выбран, иначе - ложь</returns>
        bool OpenFileDialog(out string path);

        /// <summary>
        /// Показ диалогового окна сохранения файла
        /// </summary>
        /// <param name="path">
        /// Путь к указанному файлу, если файл не указан, параметр равен null
        /// </param>
        /// <returns>Истина, если файл указан, иначе - ложь</returns>
        bool SaveFileDialog(out string path);
    }
}