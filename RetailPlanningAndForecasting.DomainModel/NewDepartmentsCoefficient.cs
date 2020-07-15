using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class NewDepartmentsCoefficient
    {
        private decimal? _value;

        public decimal? Value
        {
            get => _value;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _value = value;
            }
        }
    }
}