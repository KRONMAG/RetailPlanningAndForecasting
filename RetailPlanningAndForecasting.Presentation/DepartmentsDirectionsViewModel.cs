using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    public class DepartmentsDirectionsViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRepository<DepartmentsDirection> _repository;

        public ObservableCollection<DepartmentsDirection> Directions { get; }

        public RelayCommand AddDirectionCommand { get; }

        public RelayCommand<DepartmentsDirection> RemoveDirectionCommand { get; }

        public string DirectionName { get; set; }

        public DepartmentsDirectionsViewModel(IDialogService dialogService, IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _dialogService = dialogService;
            _repository = repositoryCreator.Create<DepartmentsDirection>();

            Directions = new ObservableCollection<DepartmentsDirection>(_repository.Get());
            AddDirectionCommand = new RelayCommand(AddDirection);
            RemoveDirectionCommand = new RelayCommand<DepartmentsDirection>(RemoveDirection);
        }

        private void AddDirection()
        {
            if (string.IsNullOrWhiteSpace(DirectionName))
                _dialogService.ShowMessage("Название направления супермаркета не может быть пустым", "Ошибка ввода");
            else if (Directions.Any(direction => direction.Name == DirectionName))
                _dialogService.ShowMessage("Направление с указанным названием уже содержится в списке", "Ошибка ввода");
            else
            {
                var newDirection = new DepartmentsDirection(DirectionName);
                _repository.Add(new[] { newDirection });
                Directions.Add(newDirection);
                DirectionName = null;
                base.RaisePropertyChanged(nameof(DirectionName));
            }
        }

        private void RemoveDirection(DepartmentsDirection direction)
        {
            Requires.NotNull(direction, nameof(direction));

            _repository.Delete(new[] { direction });
            Directions.Remove(direction);
        }
    }
}