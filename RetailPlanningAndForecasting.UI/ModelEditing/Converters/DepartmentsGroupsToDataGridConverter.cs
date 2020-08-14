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
    public class DepartmentsGroupsToDataGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = ((ReadOnlyObservableCollection<object>)value)
                .Cast<DepartmentsGroup>();

            return DataGridCreator.Create
            (
                items.ToDictionary(item => (item.DepartmentsLabel.Name, item.Year), item => item),
                CellTemplateCreator.Create("DepartmentsCount")
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
