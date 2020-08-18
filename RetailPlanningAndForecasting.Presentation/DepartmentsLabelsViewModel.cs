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
    /// Модель представления редактирования списка меток отделений торговой сети
    /// </summary>
    public sealed class DepartmentsLabelsViewModel : ViewModelBase
    {
        /// <summary>
        /// Репозиторий меток отделений
        /// </summary>
        private readonly IRepository<DepartmentsLabel> _repository;

        /// <summary>
        /// Наименование добавляемой в список метки
        /// </summary>
        private string _labelName;

        /// <summary>
        /// Следует ли для отделений с добавляемой меткой
        /// расчитывать планируемый товарооборот по формуле для новых отделений
        /// </summary>
        private bool _areDepartmentsNew;

        /// <summary>
        /// Список меток
        /// </summary>
        public ObservableCollection<DepartmentsLabel> Labels { get; }

        /// <summary>
        /// Команда добавления меток в список
        /// </summary>
        public DelegateCommand AddLabelCommand { get; }

        /// <summary>
        /// Команда удаления метки из списка
        /// </summary>
        public DelegateCommand<DepartmentsLabel> RemoveLabelCommand { get; }

        /// <summary>
        /// Наименование добавляемой метки
        /// </summary>
        public string LabelName
        {
            get => _labelName;
            set
            {
                ClearErrors(nameof(LabelName));
                SetProperty(ref _labelName, value);
                if (string.IsNullOrWhiteSpace(value))
                    AddError(nameof(LabelName), "Название метки супермаркета не может быть пустым");
                else if (Labels.Any(label => label.Name == value))
                    AddError(nameof(LabelName), "Указанная метка уже присутствует в списке");
                AddLabelCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Следует ли для отделений с добавляемой меткой
        /// расчитывать планируемый товарооборот по формуле для новых отделений
        /// </summary>
        public bool AreDepartmentsNew
        {
            get => _areDepartmentsNew;
            set => SetProperty(ref _areDepartmentsNew, value);
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repository">Репозиторий меток отделений</param>
        public DepartmentsLabelsViewModel(IRepository<DepartmentsLabel> repository)
        {
            Requires.NotNull(repository, nameof(repository));

            _repository = repository;

            Labels = new ObservableCollection<DepartmentsLabel>(repository.Get());
            AddLabelCommand = new DelegateCommand
            (
                AddLabel,
                () => !HasErrors && _labelName != null
            );
            RemoveLabelCommand = new DelegateCommand<DepartmentsLabel>(RemoveLabel);
        }

        /// <summary>
        /// Создание на основе указанных данных метки отделений,
        /// ее добавление в список, сохранение в хранилище данных
        /// </summary>
        private void AddLabel()
        {
            var newLabel = new DepartmentsLabel(_labelName, _areDepartmentsNew);
            _repository.Add(new[] { newLabel });
            Labels.Add(newLabel);
            SetProperty(ref _labelName, null, nameof(LabelName));
            SetProperty(ref _areDepartmentsNew, false, nameof(AreDepartmentsNew));
            AddLabelCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Удаление указанной метки из списка и хранилища данных
        /// </summary>
        /// <param name="label">Метка для удаления</param>
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