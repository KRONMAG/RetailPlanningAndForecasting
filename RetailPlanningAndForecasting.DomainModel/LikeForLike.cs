using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class LikeForLike
    {
        private decimal? _coefficient;

        public DepartmentsType DepartmentType { get; }

        public int Year { get; }

        public decimal? Coefficient
        {
            get => _coefficient;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _coefficient = value;
            }
        }

        public LikeForLike(DepartmentsType departmentsType, int year)
        {
            Requires.InRange(year >= 0, nameof(year));

            this.DepartmentType = departmentsType;
            this.Year = year;
        }
    }
}