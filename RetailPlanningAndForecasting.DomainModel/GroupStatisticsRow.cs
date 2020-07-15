using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class GroupStatisticsRow
    {
        private int? _departmentsCount;
        private decimal? _turnover;

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

        public decimal? Turnover
        {
            get => _turnover;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _turnover = value;
            }
        }

        public GroupStatisticsRow(int year)
        {
            Requires.InRange(year > 0, nameof(year));

            this.Year = year;
        }
    }
}