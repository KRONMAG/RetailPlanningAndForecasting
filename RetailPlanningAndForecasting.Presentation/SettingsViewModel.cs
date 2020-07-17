using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Presentation
{
    public class SettingsViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private IRepository<Region> _regionsRepository;
        private IRepository<DepartmentsDirection> _directionsRepository;

        public ObservableCollection<Region> Regions { get; }
        public ObservableCollection<DepartmentsDirection> Directions { get; }

        public RelayCommand AddRegionCommand { get; }
        public RelayCommand<Region> RemoveRegionCommand { get; }
        public RelayCommand AddDirectionCommand { get; }
        public RelayCommand<DepartmentsDirection> RemoveDirectionCommand { get; }
        public RelayCommand SaveSettingsCommand { get; }

        public string RegionName { get; set; }

        public string DirectionName { get; set; }

        public SettingsViewModel(IDialogService dialogService, IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _dialogService = dialogService;
            _regionsRepository = repositoryCreator.Create<Region>();
            _directionsRepository = repositoryCreator.Create<DepartmentsDirection>();

            Regions = new ObservableCollection<Region>(_regionsRepository.Get());
            Directions = new ObservableCollection<DepartmentsDirection>(_directionsRepository.Get());
            AddRegionCommand = new RelayCommand(AddRegion);
            RemoveRegionCommand = new RelayCommand<Region>(RemoveRegion);
            AddDirectionCommand = new RelayCommand(AddDirection);
            RemoveDirectionCommand = new RelayCommand<DepartmentsDirection>(RemoveDirection);
            SaveSettingsCommand = new RelayCommand(SaveSettings);
        }

        private void AddRegion()
        {
            if (string.IsNullOrWhiteSpace(RegionName))
                _dialogService.ShowMessage("Название региона не может быть пустым", "Ошибка ввода");
            else if (Regions.Any(region => region.Name == RegionName))
                _dialogService.ShowMessage("Регион с указанным названием уже присутствует в списке", "Ошибка ввода");
            else
            {
                Regions.Add(new Region(RegionName));
                RegionName = null;
                RaisePropertyChanged("RegionName");
            }
        }

        private void RemoveRegion(Region region) =>
            Regions.Remove(region);

        private void AddDirection()
        {
            if (string.IsNullOrWhiteSpace(DirectionName))
                _dialogService.ShowMessage("Название направления супермаркета не может быть пустым", "Ошибка ввода");
            else if (Directions.Any(direction => direction.Name == DirectionName))
                _dialogService.ShowMessage("Направление с указанным названием уже содержится в списке", "Ошибка ввода");
            else
            {
                Directions.Add(new DepartmentsDirection(DirectionName));
                DirectionName = null;
                base.RaisePropertyChanged("DirectionName");
            }
        }

        private void RemoveDirection(DepartmentsDirection direction) =>
            Directions.Remove(direction);

        private void SaveSettings()
        {
            try
            {
                _regionsRepository.Clear();
                _regionsRepository.Add(Regions);
                _directionsRepository.Clear();
                _directionsRepository.Add(Directions);
                _dialogService.ShowMessage("Настройки успешно сохранены", "Успех");
            }
            catch
            {
                _dialogService.ShowError("В ходе сохранения настроек приложения произошла ошибка", "Ошибка сохранения", "OK", delegate { });
                throw;
            }
        }
    }
}