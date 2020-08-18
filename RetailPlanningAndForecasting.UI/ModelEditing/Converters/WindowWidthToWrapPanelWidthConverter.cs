using System;
using System.Globalization;
using System.Windows.Data;

namespace RetailPlanningAndForecasting.UI.ModelEditing.Converters
{
    public sealed class WindowWidthToWrapPanelWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (double)value - 60;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}