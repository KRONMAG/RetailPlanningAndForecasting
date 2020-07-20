using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class DepartmentsGroup
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
                Requires.InRange(value == null || value >= 0, nameof(value));

                _departmentsCount = value;
            }
        }

        public decimal? PlannedTurnover
        {
            get => _plannedTurnover;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _plannedTurnover = value;
            }
        }


        public DepartmentsGroup(DepartmentsDirection direction, Region region, DepartmentsLabel label, int year)
        {
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(region, nameof(region));
            Requires.NotNull(label, nameof(label));
            Requires.InRange(year > 0, nameof(year));

            this.DepartmentsDirection = direction;
            this.Region = region;
            this.DepartmentsLabel = label;
            this.Year = year;
        }
    }
}