using System.Windows;
using System.Windows.Markup;

namespace RetailPlanningAndForecasting.UI.ModelEditing.DataGridHelpers
{
    /// <summary>
    /// Создатель шаблона данных для редактирования значений
    /// </summary>
    public class CellTemplateCreator
    {
        /// <summary>
        /// Создание шаблона данных для редактирования значений
        /// свойства с указанным именем в текстовом поле
        /// </summary>
        /// <param name="property">Наименование редактируемого свойства</param>
        /// <returns>Созданный шаблон данных</returns>
        public static DataTemplate Create(string property) =>
            (DataTemplate)XamlReader.Parse
            (
                $@"<DataTemplate 
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:mahapps=""http://metro.mahapps.com/winfx/xaml/controls""> 
                    <TextBox Text=""{{Binding {property}, TargetNullValue=0, ValidatesOnExceptions=True, UpdateSourceTrigger=PropertyChanged}}""/>
                </DataTemplate>"
            );
    }
}