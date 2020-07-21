using System.Linq;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Presentation
{
    public class PlanningPeriodViewModel : ViewModelBase
    {
        private readonly IRepository<PlanningPeriod> _repository;
        private int _startYear;
        private int _endYear;

        public DelegateCommand SavePeriodCommand { get; }

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

        public PlanningPeriodViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<PlanningPeriod>();

            var period = _repository.Get().First();
            _startYear = period.StartYear;
            _endYear = period.EndYear;
            SavePeriodCommand = new DelegateCommand(SavePeriod, () => !base.HasErrors);
        }

        private void SavePeriod()
        {
            var newPeriod = new PlanningPeriod(_startYear, _endYear);
            _repository.Clear();
            _repository.Add(new[] { newPeriod });
        }
    }
}