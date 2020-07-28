using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using Gu.Wpf.DataGrid2D;

namespace RetailPlanningAndForecasting.UI.ModelInitialization
{
    public class DataGridCreator
    {
        public static DataGrid Create<TRowName, TColumnName, TValue>
            (Dictionary<(TRowName, TColumnName), TValue> items,
            DataTemplate template)
        {
            var dataGrid = new DataGrid();
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
            dataGrid.SetColumnHeadersSource(columnNames);
            dataGrid.SetRowHeadersSource(rowNames);
            dataGrid.SetArray2D(array);
            dataGrid.SetTemplate(template);
            return dataGrid;
        }
    }
}
