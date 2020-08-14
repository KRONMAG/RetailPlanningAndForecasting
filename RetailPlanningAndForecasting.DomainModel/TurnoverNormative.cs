using CodeContracts;
using Prism.Mvvm;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Нормативный товарооборот
    /// </summary>
    public class TurnoverNormative : BindableBase
    {
        /// <summary>
        /// Значение нормативного товарооборота
        /// </summary>
        private decimal? _normativeTurnover;

        /// <summary>
        /// Регион размещения отделений
        /// </summary>
        public Region Region { get; }

        /// <summary>
        /// Направление отделений
        /// </summary>
        public DepartmentsDirection DepartmentsDirection { get; }

        /// <summary>
        /// Метка отделений
        /// </summary>
        public DepartmentsLabel DepartmentsLabel { get; }

        /// <summary>
        /// Значение нормативного товарооборота
        /// </summary>
        public decimal? NormativeTurnover
        {
            get => _normativeTurnover;
            set
            {
                Requires.True(value == null || value >= 0, "Нормативный товарооборот не может быть отрицательным");

                base.SetProperty(ref _normativeTurnover, value);
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="region">Регион размещения отделений</param>
        /// <param name="direction">Направление отделений</param>
        /// <param name="label">Метка отделений торговой</param>
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