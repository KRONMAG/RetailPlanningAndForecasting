using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.DomainModel;

namespace RetailPlanningAndForecasting.Presentation
{
    public class RegionsViewModel : ViewModelBase
    {
        private readonly IRepository<Region> _repository;
        private string _regionName;

        public ObservableCollection<Region> Regions { get; }

        public DelegateCommand AddRegionCommand { get; }

        public DelegateCommand<Region> RemoveRegionCommand { get; }

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

        public RegionsViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<Region>();

            Regions = new ObservableCollection<Region>(_repository.Get());
            AddRegionCommand = new DelegateCommand(AddRegion, () => !base.HasErrors && RegionName != null);
            RemoveRegionCommand = new DelegateCommand<Region>(RemoveRegion);
        }

        private void AddRegion()
        {
            var newRegion = new Region(_regionName);
            _repository.Add(new[] { newRegion });
            Regions.Add(newRegion);
            base.SetProperty(ref _regionName, null, nameof(RegionName));
            AddRegionCommand.RaiseCanExecuteChanged();
        }

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