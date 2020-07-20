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
    public class RegionsViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRepository<Region> _repository;

        public ObservableCollection<Region> Regions { get; }

        public RelayCommand AddRegionCommand { get; }

        public RelayCommand<Region> RemoveRegionCommand { get; }

        public string RegionName { get; set; }

        public RegionsViewModel(IDialogService dialogService, IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _dialogService = dialogService;
            _repository = repositoryCreator.Create<Region>();

            Regions = new ObservableCollection<Region>(_repository.Get());
            AddRegionCommand = new RelayCommand(AddRegion);
            RemoveRegionCommand = new RelayCommand<Region>(RemoveRegion);
        }

        private void AddRegion()
        {
            if (string.IsNullOrWhiteSpace(RegionName))
                _dialogService.ShowMessage("Название региона не может быть пустым", "Ошибка ввода");
            else if (Regions.Any(region => region.Name == RegionName))
                _dialogService.ShowMessage("Регион с указанным названием уже присутствует в списке", "Ошибка ввода");
            else
            {
                var newRegion = new Region(RegionName);
                _repository.Add(new[] { newRegion });
                Regions.Add(newRegion);
                RegionName = null;
                RaisePropertyChanged(nameof(RegionName));
            }
        }

        private void RemoveRegion(Region region)
        {
            Requires.NotNull(region, nameof(region));

            _repository.Delete(new[] { region });
            Regions.Remove(region);
        }
    }
}