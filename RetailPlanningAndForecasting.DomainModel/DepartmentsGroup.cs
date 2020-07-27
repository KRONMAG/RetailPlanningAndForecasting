using Prism.Mvvm;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class DepartmentsGroup : BindableBase
    {
        private int? _departmentsCount;
        private decimal? _plannedTurnover;

        public Region Region { get; }

        public DepartmentsDirection DepartmentsDirection { get; }

        public DepartmentsLabel DepartmentsLabel { get; }

        public int Year { get; }

        public int? DepartmentsCount
        {
            get => _departmentsCount;
            set
            {
                Requires.InRange(value == null || value >= 0, nameof(value), "Количество супермаркетов не может быть отрицательным");

                base.SetProperty(ref _departmentsCount, value);
            }
        }

        public decimal? PlannedTurnover
        {
            get => _plannedTurnover;
            set
            {
                Requires.InRange(value == null || value >= 0, nameof(value), "Прогнозируемый товарооборот не может быть отрицательным");

                base.SetProperty(ref _plannedTurnover, value);
            }
        }


        public DepartmentsGroup(Region region, DepartmentsDirection direction, DepartmentsLabel label, int year)
        {
            Requires.NotNull(region, nameof(region));
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(label, nameof(label));
            Requires.InRange(year > 0, nameof(year));

            this.Region = region;
            this.DepartmentsLabel = label;
            this.Year = year;
        }
    }
}