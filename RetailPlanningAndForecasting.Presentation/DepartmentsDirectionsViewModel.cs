using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    public class DepartmentsDirectionsViewModel : ViewModelBase
    {
        private readonly IRepository<DepartmentsDirection> _repository;
        private string _directionName;

        public ObservableCollection<DepartmentsDirection> Directions { get; }

        public DelegateCommand AddDirectionCommand { get; }

        public DelegateCommand<DepartmentsDirection> RemoveDirectionCommand { get; }

        public string DirectionName
        {
            get => _directionName;
            set
            {
                base.ClearErrors(nameof(DirectionName));
                base.SetProperty(ref _directionName, value);
                if (string.IsNullOrWhiteSpace(value))
                    base.AddError(nameof(DirectionName), "Название направления супермаркета не может быть пустым");
                else if (Directions.Any(direction => direction.Name == value))
                    base.AddError(nameof(DirectionName), "Указанное направление уже содержится в списке");
                AddDirectionCommand.RaiseCanExecuteChanged();
            }
        }

        public DepartmentsDirectionsViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<DepartmentsDirection>();

            Directions = new ObservableCollection<DepartmentsDirection>(_repository.Get());
            AddDirectionCommand = new DelegateCommand
            (
                AddDirection, () => !base.HasErrors && _directionName !=null
            );
            RemoveDirectionCommand = new DelegateCommand<DepartmentsDirection>(RemoveDirection);
        }

        private void AddDirection()
        {
            var newDirection = new DepartmentsDirection(_directionName);
            _repository.Add(new[] { newDirection });
            Directions.Add(newDirection);
            SetProperty(ref _directionName, null, nameof(DirectionName));
            AddDirectionCommand.RaiseCanExecuteChanged();
        }

        private void RemoveDirection(DepartmentsDirection direction)
        {
            if (direction != null && Directions.Contains(direction))
            {
                _repository.Delete(new[] { direction });
                Directions.Remove(direction);
            }
        }
    }
}