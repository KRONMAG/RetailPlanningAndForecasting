using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class TurnoverNormative
    {
        private decimal? _normativeTurnover;

        public Region Region { get; }

        public DepartmentsDirection DepartmentDirection { get; }

        public DepartmentsLabel DepartmentsLabel { get; }

        public decimal? NormativeTurnover
        {
            get => _normativeTurnover;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _normativeTurnover = value;
            }
        }

        public TurnoverNormative(Region region, DepartmentsDirection direction, DepartmentsLabel label)
        {
            Requires.NotNull(region, nameof(region));
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(label, nameof(label));

            this.Region = region;
            this.DepartmentDirection = direction;
            this.DepartmentsLabel = label;
        }
    }
}