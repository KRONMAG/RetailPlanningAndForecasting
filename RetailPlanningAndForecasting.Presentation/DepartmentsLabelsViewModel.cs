using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    public class DepartmentsLabelsViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IRepository<DepartmentsLabel> _repository;

        public ObservableCollection<DepartmentsLabel> Labels { get; }

        public RelayCommand AddLabelCommand { get; }

        public RelayCommand<DepartmentsLabel> RemoveLabelCommand { get; }

        public string LabelName { get; set; }

        public bool AreDepartmentsNew { get; set; }

        public DepartmentsLabelsViewModel(IDialogService dialogService, IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _dialogService = dialogService;
            _repository = repositoryCreator.Create<DepartmentsLabel>();

            Labels = new ObservableCollection<DepartmentsLabel>(_repository.Get());
            AddLabelCommand = new RelayCommand(AddLabel);
            RemoveLabelCommand = new RelayCommand<DepartmentsLabel>(RemoveLabel);
        }

        private void AddLabel()
        {
            if (string.IsNullOrWhiteSpace(LabelName))
                _dialogService.ShowMessage("Метка супермаркета не может быть пустой", "Ошибка ввода");
            else if (Labels.Any(label => label.Name == LabelName))
                _dialogService.ShowMessage("Метка с указанным именем уже присутствует в списке", "Ошибка ввода");
            else
            {
                var newLabel = new DepartmentsLabel(LabelName, AreDepartmentsNew);

                _repository.Add(new[] { newLabel });
                Labels.Add(newLabel);
                LabelName = null;
                base.RaisePropertyChanged(nameof(LabelName));
                AreDepartmentsNew = false;
                base.RaisePropertyChanged(nameof(AreDepartmentsNew));
            }
        }

        private void RemoveLabel(DepartmentsLabel label)
        {
            Requires.NotNull(label, nameof(label));

            _repository.Delete(new[] { label });
            Labels.Remove(label);
        }
    }
}