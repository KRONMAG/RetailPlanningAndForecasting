using Prism.Mvvm;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class NewDepartmentsCoefficient : BindableBase
    {
        private decimal? _value;

        public decimal? Value
        {
            get => _value;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value), "Коэффициент новых магазинов должен быть больше нуля");

                SetProperty(ref _value, value);
            }
        }
    }
}