using System.Linq;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования периода планирования товарооборота
    /// </summary>
    public sealed class PlanningPeriodViewModel : ViewModelBase
    {
        /// <summary>
        /// Репозиторий периода планирования
        /// </summary>
        private readonly IRepository<PlanningPeriod> _repository;

        /// <summary>
        /// Год начала периода планирования
        /// </summary>
        private int _startYear;

        /// <summary>
        /// Год окончания периода планирования
        /// </summary>
        private int _endYear;

        /// <summary>
        /// Команда сохранения периода планирования
        /// </summary>
        public DelegateCommand SavePeriodCommand { get; }

        /// <summary>
        /// Год начала периода планирования
        /// </summary>
        public int StartYear
        {
            get => _startYear;
            set
            {
                ClearErrors(nameof(StartYear));
                SetProperty(ref _startYear, value);
                if (value <= 0)
                    AddError(nameof(StartYear), "Год начала периода должен быть положительным числом");
                else if (value > _endYear)
                    AddError(nameof(StartYear), "Год начала периода не может быть больше года его конца");
            }
        }

        /// <summary>
        /// Год окончания периода планирования
        /// </summary>
        public int EndYear
        {
            get => _endYear;
            set
            {
                ClearErrors(nameof(EndYear));
                SetProperty(ref _endYear, value);
                if (value <= 0)
                    AddError(nameof(EndYear), "Год конца периода должет быть положительным числом");
                else if (value < _startYear)
                    AddError(nameof(EndYear), "Год конца периода не может быть меньше года его начала");
                SavePeriodCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repository">Репозиторий периода планирования товарооборота</param>
        public PlanningPeriodViewModel(IRepository<PlanningPeriod> repository)
        {
            Requires.NotNull(repository, nameof(repository));

            _repository = repository;

            var period = repository.Get().First();
            _startYear = period.StartYear;
            _endYear = period.EndYear;
            SavePeriodCommand = new DelegateCommand(SavePeriod, () => !HasErrors);
        }

        /// <summary>
        /// Создание периода планирования на основе указанных годов его начала и конца,
        /// добавление периода в список, хранилище данных
        /// </summary>
        private void SavePeriod()
        {
            var newPeriod = new PlanningPeriod(_startYear, _endYear);
            _repository.Clear();
            _repository.Add(new[] { newPeriod });
        }
    }
}