using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class NormativeTurnover
    {
        private decimal? _oldDepartmentTurnover;
        private decimal? _newDepartmentTurnover;

        public virtual DepartmentsDirection DepartmentDirection { get; }

        public virtual Region Region { get; }

        public decimal? OldDepartmentTurnover
        {
            get => _oldDepartmentTurnover;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _oldDepartmentTurnover = value;
            }
        }

        public decimal? NewDepartmentTurnover
        {
            get => _newDepartmentTurnover;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _newDepartmentTurnover = value;
            }
        }

        public NormativeTurnover(DepartmentsDirection departmentDirection, Region region)
        {
            Requires.NotNull(departmentDirection, nameof(departmentDirection));
            Requires.NotNull(region, nameof(region));

            this.DepartmentDirection = departmentDirection;
            this.Region = region;
        }
    }
}