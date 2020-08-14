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
    /// Конвертер списка LikeForLike-коэффициентов в элемент управления DataGrid
    /// </summary>
    public class LikeForLikesToDataGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var items = (IReadOnlyList<LikeForLike>)value;

            return DataGridCreator.Create
            (
                items.ToDictionary(item => (item.DepartmentsLabel.Name, item.Year), item => item),
                CellTemplateCreator.Create("Coefficient")
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}