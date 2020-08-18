using System.Linq;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления инициализации модели планирования товарооборота
    /// </summary>
    public sealed class ModelInitializationViewModel : ViewModelBase
    {
        /// <summary>
        /// Контроллер приложения
        /// </summary>
        private readonly ApplicationController _controller;

        /// <summary>
        /// Создатель репозиториев
        /// </summary>
        private readonly IRepositoryCreator _repositoryCreator;

        /// <summary>
        /// Средство показа диалоговых окон
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Средство записи объектов в файл, чтения объектов из файла
        /// </summary>
        private readonly ISerializeStream _serializeStream;

        /// <summary>
        /// Модель представления редактирования периода планирования товарооборота
        /// </summary>
        public PlanningPeriodViewModel PlanningPeriodViewModel { get; }

        /// <summary>
        /// Модель представления редактирования списка регионов размещения отделений
        /// </summary>
        public RegionsViewModel RegionsViewModel { get; }

        /// <summary>
        /// Модель представления редактирования меток отделений
        /// </summary>
        public DepartmentsLabelsViewModel DepartmentsLabelsViewModel { get; }

        /// <summary>
        /// Модель представления редактирования списка направлений
        /// </summary>
        public DepartmentsDirectionsViewModel DepartmentsDirectionsViewModel { get; }

        /// <summary>
        /// Команда создания новой модели планирования товарооборота
        /// </summary>
        public DelegateCommand CreateModelCommand { get; }

        /// <summary>
        /// Команда загрузки существующей модели планирования товарооборота из файла
        /// </summary>
        public DelegateCommand LoadModelCommand { get; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
        /// <param name="dialogService">Средство показа диалоговых окон</param>
        /// <param name="serializeStream">Средство записи объектов в файлы, чтения объектов из файла</param>
        public ModelInitializationViewModel
            (ApplicationController controller,
            IRepositoryCreator repositoryCreator,
            IDialogService dialogService,
            ISerializeStream serializeStream)
        {
            Requires.NotNull(controller, nameof(controller));
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(serializeStream, nameof(serializeStream));

            _repositoryCreator = repositoryCreator;
            _dialogService = dialogService;
            _serializeStream = serializeStream;
            _controller = controller;

            LoadModelCommand = new DelegateCommand(LoadModel);
            CreateModelCommand = new DelegateCommand(CreateModel);
            PlanningPeriodViewModel = new PlanningPeriodViewModel
            (
                repositoryCreator.Create<PlanningPeriod>()
            );
            RegionsViewModel = new RegionsViewModel
            (
                repositoryCreator.Create<Region>()
            );
            DepartmentsLabelsViewModel = new DepartmentsLabelsViewModel
            (
                repositoryCreator.Create<DepartmentsLabel>()
            );
            DepartmentsDirectionsViewModel = new DepartmentsDirectionsViewModel
            (
                repositoryCreator.Create<DepartmentsDirection>()
            );
        }

        /// <summary>
        /// Создание модели планирования товарооборота на основе указанных данных
        /// </summary>
        private void CreateModel()
        {
            _controller.RegisterInstance
            (
                new TurnoverModel
                (
                    _repositoryCreator.Create<Region>().Get(),
                    _repositoryCreator.Create<DepartmentsDirection>().Get(),
                    _repositoryCreator.Create<DepartmentsLabel>().Get(),
                    _repositoryCreator.Create<PlanningPeriod>().Get().First()
                )
            );
            _controller.Run<ModelEditingViewModel>();
        }

        /// <summary>
        /// Загрузка модели планирования товарооборота из указанного файла
        /// </summary>
        private void LoadModel()
        {
            try
            {
                if (_dialogService.OpenFileDialog(out string path))
                {
                    _controller.RegisterInstance(_serializeStream.Read<TurnoverModel>(path).First());
                    _controller.Run<ModelEditingViewModel>();
                }
            }
            catch
            {
                _dialogService.MessageDialog("Ошибка", "В ходе загрузки модели товарооборота произошла ошибка");
            }
        }
    }
}