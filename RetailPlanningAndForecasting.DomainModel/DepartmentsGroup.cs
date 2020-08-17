using System;
using Prism.Mvvm;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Статистика по группе отделений торговой сети за год
    /// Группу образуют отделения с одинаковым регионом размещения, направлением и меткой
    /// </summary>
    [Serializable]
    public class DepartmentsGroup : BindableBase
    {
        /// <summary>
        /// Количество отделений
        /// </summary>
        private int? _departmentsCount;

        /// <summary>
        /// Планируемый товарооборот
        /// </summary>
        private decimal? _plannedTurnover;

        /// <summary>
        /// Наименование региона, в котором размещены отделения
        /// </summary>
        public Region Region { get; private set; }

        /// <summary>
        /// Направление отделений
        /// </summary>
        public DepartmentsDirection DepartmentsDirection { get; private set; }

        /// <summary>
        /// Метка отделений
        /// </summary>
        public DepartmentsLabel DepartmentsLabel { get; private set; }

        /// <summary>
        /// Год, для которого указано количество отделений и планируемый товарооборот
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Количество отделений
        /// </summary>
        public int? DepartmentsCount
        {
            get => _departmentsCount;
            set
            {
                Requires.True(value == null || value >= 0, "Количество супермаркетов не может быть отрицательным");

                base.SetProperty(ref _departmentsCount, value);
            }
        }

        /// <summary>
        /// Планируемый товарооборот
        /// </summary>
        public decimal? PlannedTurnover
        {
            get => _plannedTurnover;
            internal set
            {
                Requires.True(value == null || value >= 0, "Прогнозируемый товарооборот не может быть отрицательным");

                base.SetProperty(ref _plannedTurnover, value);
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="region">Регион, в котором размещены отделения</param>
        /// <param name="direction">Направление отделений</param>
        /// <param name="label">Метка отделений</param>
        /// <param name="year">Год ведения статистики</param>
        public DepartmentsGroup(Region region, DepartmentsDirection direction, DepartmentsLabel label, int year)
        {
            Requires.NotNull(region, nameof(region));
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(label, nameof(label));
            Requires.InRange(year > 0, nameof(year));

            this.Region = region;
            this.DepartmentsDirection = direction;
            this.DepartmentsLabel = label;
            this.Year = year;
        }

        private DepartmentsGroup()
        {

        }
    }
}