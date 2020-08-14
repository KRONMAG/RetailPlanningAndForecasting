using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования списка регионов размещения
    /// отделений торговой сети
    /// </summary>
    public class RegionsViewModel : ViewModelBase
    {
        /// <summary>
        /// Репозиторий регионов
        /// </summary>
        private readonly IRepository<Region> _repository;

        /// <summary>
        /// Наименование добавляемого региона
        /// </summary>
        private string _regionName;

        /// <summary>
        /// Список регионов
        /// </summary>
        public ObservableCollection<Region> Regions { get; }

        /// <summary>
        /// Команда добавления региона в список
        /// </summary>
        public DelegateCommand AddRegionCommand { get; }

        /// <summary>
        /// Команда удаления региона из списка
        /// </summary>
        public DelegateCommand<Region> RemoveRegionCommand { get; }

        /// <summary>
        /// Наименование добавляемого региона
        /// </summary>
        public string RegionName
        {
            get => _regionName;
            set
            {
                base.ClearErrors(nameof(RegionName));
                base.SetProperty(ref _regionName, value);
                if (string.IsNullOrWhiteSpace(value))
                    base.AddError(nameof(RegionName), "Название региона не может быть пустым");
                else if (Regions.Any(region => region.Name == value))
                    base.AddError(nameof(RegionName), "Указанный регион уже присутствует в списке");
                AddRegionCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
        public RegionsViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<Region>();

            Regions = new ObservableCollection<Region>(_repository.Get());
            AddRegionCommand = new DelegateCommand(AddRegion, () => !base.HasErrors && RegionName != null);
            RemoveRegionCommand = new DelegateCommand<Region>(RemoveRegion);
        }

        /// <summary>
        /// Создание региона с указанным наименованием, добавление его в список, хранилище данных
        /// </summary>
        private void AddRegion()
        {
            var newRegion = new Region(_regionName);
            _repository.Add(new[] { newRegion });
            Regions.Add(newRegion);
            base.SetProperty(ref _regionName, null, nameof(RegionName));
            AddRegionCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Удаление указанного региона из списка, хранилища данных
        /// </summary>
        /// <param name="region">Удаляемый регион</param>
        private void RemoveRegion(Region region)
        {
            if (region != null && Regions.Contains(region))
            {
                _repository.Delete(new[] { region });
                Regions.Remove(region);
            }
        }
    }
}