using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.UI.ModelEditing.DataGridHelpers;

namespace RetailPlanningAndForecasting.UI.ModelEditing.Converters
{
    /// <summary>
    /// Конвертер списка нормативов товарооборота в элемент управления DataGrid
    /// </summary>
    public class TurnoverNormativesToDataGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var turnoverNormatives = ((IEnumerable<object>)value).Cast<TurnoverNormative>();

            return DataGridCreator.Create
            (
                turnoverNormatives.ToDictionary
                (
                    item => (item.DepartmentsLabel.Name, item.DepartmentsDirection.Name),
                    item => item
                ),
                CellTemplateCreator.Create("NormativeTurnover")
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}