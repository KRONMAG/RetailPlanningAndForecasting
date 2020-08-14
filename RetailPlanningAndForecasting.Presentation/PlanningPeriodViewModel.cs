using System.Linq;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования периода планирования товарооборота
    /// </summary>
    public class PlanningPeriodViewModel : ViewModelBase
    {
        /// <summary>
        /// Создатель репозиториев
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
                base.ClearErrors(nameof(StartYear));
                base.SetProperty(ref _startYear, value);
                if (value <= 0)
                    base.AddError(nameof(StartYear), "Год начала периода должен быть положительным числом");
                else if (value > _endYear)
                    base.AddError(nameof(StartYear), "Год начала периода не может быть больше года его конца");
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
                base.ClearErrors(nameof(EndYear));
                base.SetProperty(ref _endYear, value);
                if (value <= 0)
                    base.AddError(nameof(EndYear), "Год конца периода должет быть положительным числом");
                else if (value < _startYear)
                    base.AddError(nameof(EndYear), "Год конца периода не может быть меньше года его начала");
                SavePeriodCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
        public PlanningPeriodViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<PlanningPeriod>();

            var period = _repository.Get().First();
            _startYear = period.StartYear;
            _endYear = period.EndYear;
            SavePeriodCommand = new DelegateCommand(SavePeriod, () => !base.HasErrors);
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