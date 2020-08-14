using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования списка направлений отделений торговой сети
    /// </summary>
    public class DepartmentsDirectionsViewModel : ViewModelBase
    {
        /// <summary>
        /// Репозиторий направлений отделений
        /// </summary>
        private readonly IRepository<DepartmentsDirection> _repository;

        /// <summary>
        /// Название добавляемого направления
        /// </summary>
        private string _directionName;

        /// <summary>
        /// Список направлений
        /// </summary>
        public ObservableCollection<DepartmentsDirection> Directions { get; }

        /// <summary>
        /// Команда добавления направления в список
        /// </summary>
        public DelegateCommand AddDirectionCommand { get; }

        /// <summary>
        /// Команда удаления направления из списка
        /// </summary>
        public DelegateCommand<DepartmentsDirection> RemoveDirectionCommand { get; }

        /// <summary>
        /// Наименование добавляемого направления
        /// </summary>
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

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
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

        /// <summary>
        /// Создание направления с указанным наименованием
        /// с добавлением его в список и хранилище данных
        /// </summary>
        private void AddDirection()
        {
            var newDirection = new DepartmentsDirection(_directionName);
            _repository.Add(new[] { newDirection });
            Directions.Add(newDirection);
            SetProperty(ref _directionName, null, nameof(DirectionName));
            AddDirectionCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Удаление указанного направления из списка, хранилища данных
        /// </summary>
        /// <param name="direction">Удаляемое направление</param>
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