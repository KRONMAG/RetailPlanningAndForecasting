using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Collections.ObjectModel;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.UI.ModelEditing.DataGridHelpers;

namespace RetailPlanningAndForecasting.UI.ModelEditing.Converters
{
    /// <summary>
    /// Конвертер списка групп отделений в элемент управления DataGrid
    /// </summary>
    public sealed class DepartmentsGroupsToDataGridConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var items = ((ReadOnlyObservableCollection<object>)values[0])
                .Cast<DepartmentsGroup>();

            return DataGridCreator.Create
            (
                items.ToDictionary(item => (item.DepartmentsLabel.Name, item.Year), item => item),
                (string)values[1] == "DepartmentsCount" ?
                    CellTemplateCreator.Create((string)values[1], false) :
                    CellTemplateCreator.Create((string)values[1], true)
            );
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
