using CodeContracts;
using Prism.Mvvm;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class TurnoverNormative : BindableBase
    {
        private decimal? _normativeTurnover;

        public Region Region { get; }

        public DepartmentsDirection DepartmentsDirection { get; }

        public DepartmentsLabel DepartmentsLabel { get; }

        public decimal? NormativeTurnover
        {
            get => _normativeTurnover;
            set
            {
                Requires.InRange(value == null || value >= 0, nameof(value), "Нормативный товарооборот не может быть отрицательным");

                base.SetProperty(ref _normativeTurnover, value);
            }
        }

        public TurnoverNormative(Region region, DepartmentsDirection direction, DepartmentsLabel label)
        {
            Requires.NotNull(region, nameof(region));
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(label, nameof(label));

            this.Region = region;
            this.DepartmentsDirection = direction;
            this.DepartmentsLabel = label;
        }
    }
}