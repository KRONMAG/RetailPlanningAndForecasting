using System.Windows;
using System.Windows.Markup;

namespace RetailPlanningAndForecasting.UI.ModelEditing.DataGridHelpers
{
    /// <summary>
    /// Создатель шаблона данных показа содержимого свойства числового типа
    /// </summary>
    public static class CellTemplateCreator
    {
        /// <summary>
        /// Создание шаблона данных для отображения свойства с указанным именем
        /// </summary>
        /// <param name="property">Наименование отображаемого свойства</param>
        /// <param name="isReadOnly">Разрешено ли редактирование значения свойства</param>
        /// <returns>Созданный шаблон данных</returns>
        public static DataTemplate Create(string property, bool isReadOnly = false) =>
            (DataTemplate)XamlReader.Parse
            (
                $@"<DataTemplate 
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:mahapps=""http://metro.mahapps.com/winfx/xaml/controls""> 
                    <mahapps:NumericUpDown Value=""{{Binding {property},
                        Mode={(isReadOnly ? "OneWay" : "TwoWay")},
                        TargetNullValue=0,
                        ValidatesOnExceptions=True,
                        UpdateSourceTrigger=PropertyChanged}}""
                        Minimum=""0""
                        HideUpDownButtons=""True""
                        IsReadOnly=""{isReadOnly}""/>
                </DataTemplate>"
            );
    }
}