using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using RetailPlanningAndForecasting.DomainModel;
using Gu.Wpf.DataGrid2D;
using System.IO;
using System.Xml;
using System.Windows.Markup;
using RetailPlanningAndForecasting.UI.ModelInitialization;
using DevExpress.Xpf.PivotGrid.Automation;

namespace RetailPlanningAndForecasting.UI
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stackPanel = new StackPanel();
            ((CollectionViewGroup)value).Items
                .Cast<CollectionViewGroup>()
                .Select(group => new Expander
                {
                    Header = group.Name,
                    Margin = new Thickness(5),
                    Content = DataGridCreator.Create
                    (
                        group.Items
                            .Cast<DepartmentsGroup>()
                            .ToDictionary(item => (item.DepartmentsLabel.Name, item.Year), item => item),
                        CellTemplateCreator.Create
                        (
                            "DepartmentsCount",
                            0,
                            1
                        )
                    )
                }).ToList()
                .ForEach(expander => stackPanel.Children.Add(expander));
            return stackPanel;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
