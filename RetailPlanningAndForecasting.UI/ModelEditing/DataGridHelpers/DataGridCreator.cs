using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Gu.Wpf.DataGrid2D;

namespace RetailPlanningAndForecasting.UI.ModelEditing.DataGridHelpers
{
    /// <summary>
    /// Создатель элемента управления DataGrid
    /// </summary>
    public static class DataGridCreator
    {
        /// <summary>
        /// Создание элемента управления DataGrid на основе словаря,
        /// ключ которого - кортеж из двух элементов:
        /// - первый элемент кортежа - наименование строки таблицы
        /// - второй элемент кортежа - наименование столбца таблицы
        /// </summary>
        /// <typeparam name="TRowName">Тип имени строки таблицы</typeparam>
        /// <typeparam name="TColumnName">Тип имени столбца таблицыс</typeparam>
        /// <typeparam name="TValue">Тип данных ячейки таблицы</typeparam>
        /// <param name="items">Словарь, преобразуемый в DataGrid</param>
        /// <param name="template">Шаблон данных для отображения ячейки таблицы</param>
        /// <returns>Созданный элемент управления DataGrid</returns>
        public static DataGrid Create<TRowName, TColumnName, TValue>
            (Dictionary<(TRowName, TColumnName), TValue> items,
            DataTemplate template)
        {
            var rowNames = items.Keys
                .Select(key => key.Item1)
                .Distinct()
                .OrderBy(item => item)
                .ToArray();
            var columnNames = items.Keys
                .Select(key => key.Item2)
                .Distinct()
                .OrderBy(item => item)
                .ToArray();
            var array = new TValue[rowNames.Length, columnNames.Length];
            for (var i = 0; i < rowNames.Length; i++)
                for (var j = 0; j < columnNames.Length; j++)
                    array[i, j] = items[(rowNames[i], columnNames[j])];
            var dataGrid = new DataGrid();
            dataGrid.CanUserResizeColumns = false;
            dataGrid.HeadersVisibility = DataGridHeadersVisibility.All;
            dataGrid.SetColumnHeadersSource(GetHeaders(columnNames));
            dataGrid.SetRowHeadersSource(GetHeaders(rowNames));
            dataGrid.SetArray2D(array);
            dataGrid.SetTemplate(template);
            return dataGrid;
        }

        /// <summary>
        /// Создание массива меток на основе массива заголовков
        /// </summary>
        /// <typeparam name="T">Тип данных заголовка</typeparam>
        /// <param name="names">Массив заголовков</param>
        /// <returns>
        /// Массив меток, каждая метка содержит наименование заголовка
        /// </returns>
        private static Label[] GetHeaders<T>(T[] names) =>
            names.Select
            (
                name => new Label
                {
                    Content = name,
                }
            ).ToArray();
    }
}
