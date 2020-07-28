using System.Windows;
using System.Windows.Markup;

namespace RetailPlanningAndForecasting.UI
{
    public class CellTemplateCreator
    {
        public static DataTemplate Create(string property, decimal minimum, decimal interval) =>
            (DataTemplate)XamlReader.Parse
            (
                $@"<DataTemplate 
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:mahapps=""http://metro.mahapps.com/winfx/xaml/controls""> 
                    <mahapps:NumericUpDown Value=""{{Binding {property}, TargetNullValue=0, ValidatesOnExceptions=True}}""
                                           Width=""80""
                                           Minimum=""{minimum}""
                                           Interval=""{interval}""/>
                </DataTemplate>"
            );
    }
}