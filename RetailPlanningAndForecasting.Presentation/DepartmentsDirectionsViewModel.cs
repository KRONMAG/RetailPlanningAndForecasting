using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования списка направлений отделений торговой сети
    /// </summary>
    public sealed class DepartmentsDirectionsViewModel : ViewModelBase
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
                ClearErrors(nameof(DirectionName));
                SetProperty(ref _directionName, value);
                if (string.IsNullOrWhiteSpace(value))
                    AddError(nameof(DirectionName), "Название направления супермаркета не может быть пустым");
                else if (Directions.Any(direction => direction.Name == value))
                    AddError(nameof(DirectionName), "Указанное направление уже содержится в списке");
                AddDirectionCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repository">Репозиторий направлений отделений</param>
        public DepartmentsDirectionsViewModel(IRepository<DepartmentsDirection> repository)
        {
            Requires.NotNull(repository, nameof(repository));

            _repository = repository;

            Directions = new ObservableCollection<DepartmentsDirection>(repository.Get());
            AddDirectionCommand = new DelegateCommand
            (
                AddDirection, () => !HasErrors && _directionName !=null
            );
            RemoveDirectionCommand = new DelegateCommand<DepartmentsDirection>(RemoveDirection);
        }

        /// <summary>
        /// Создание направления с указанным наименованием с добавлением его в список и хранилище данных
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