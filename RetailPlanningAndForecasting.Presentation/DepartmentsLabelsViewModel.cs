using System.Linq;
using System.Collections.ObjectModel;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    public class DepartmentsLabelsViewModel : ViewModelBase
    {
        private readonly IRepository<DepartmentsLabel> _repository;
        private string _labelName;
        private bool _areDepartmentsNew;

        public ObservableCollection<DepartmentsLabel> Labels { get; }

        public DelegateCommand AddLabelCommand { get; }

        public DelegateCommand<DepartmentsLabel> RemoveLabelCommand { get; }

        public string LabelName
        {
            get => _labelName;
            set
            {
                base.ClearErrors(nameof(LabelName));
                SetProperty(ref _labelName, value);
                if (string.IsNullOrWhiteSpace(value))
                    base.AddError(nameof(LabelName), "Название метки супермаркета не может быть пустым");
                else if (Labels.Any(label => label.Name == value))
                    base.AddError(nameof(LabelName), "Указанная метка уже присутствует в списке");
                AddLabelCommand.RaiseCanExecuteChanged();
            }
        }

        public bool AreDepartmentsNew
        {
            get => _areDepartmentsNew;
            set => SetProperty(ref _areDepartmentsNew, value);
        }

        public DepartmentsLabelsViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _repository = repositoryCreator.Create<DepartmentsLabel>();

            Labels = new ObservableCollection<DepartmentsLabel>(_repository.Get());
            AddLabelCommand = new DelegateCommand
            (
                AddLabel,
                () => !base.HasErrors && _labelName != null
            );
            RemoveLabelCommand = new DelegateCommand<DepartmentsLabel>(RemoveLabel);
        }

        private void AddLabel()
        {
            var newLabel = new DepartmentsLabel(_labelName, _areDepartmentsNew);
            _repository.Add(new[] { newLabel });
            Labels.Add(newLabel);
            SetProperty(ref _labelName, null, nameof(LabelName));
            SetProperty(ref _areDepartmentsNew, false, nameof(AreDepartmentsNew));
            AddLabelCommand.RaiseCanExecuteChanged();
        }

        private void RemoveLabel(DepartmentsLabel label)
        {
            if (label != null && Labels.Contains(label))
            {
                _repository.Delete(new[] { label });
                Labels.Remove(label);
            }
        }
    }
}