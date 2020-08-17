using System;
using Prism.Mvvm;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Коэффициент новых отделений, учитываемый при расчете планируемого товарооборота
    /// </summary>
    [Serializable]
    public class NewDepartmentsCoefficient : BindableBase
    {
        /// <summary>
        /// Значение коэффициента
        /// </summary>
        private decimal? _value;

        /// <summary>
        /// Значение коэффициента
        /// </summary>
        public decimal? Value
        {
            get => _value;
            set
            {
                Requires.True(value == null || value > 0, "Коэффициент новых магазинов должен быть больше нуля");

                SetProperty(ref _value, value);
            }
        }
    }
}