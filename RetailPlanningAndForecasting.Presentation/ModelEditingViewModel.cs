using System.Collections.Generic;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Presentation.Common;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования модели планирования товарооборота
    /// </summary>
    public sealed class ModelEditingViewModel : ViewModelBase
    {
        /// <summary>
        /// Модель планирования товарооборота
        /// </summary>
        private readonly TurnoverModel _model;

        /// <summary>
        /// Средство показа диалоговых окон
        /// </summary>
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Средство записи объектов в файл, их чтения из файла
        /// </summary>
        private readonly ISerializeStream _serializeStream;

        /// <summary>
        /// LikeForLike-коэффициенты отделений
        /// </summary>
        public IReadOnlyList<LikeForLike> LikeForLikes { get; }

        /// <summary>
        /// Нормативные товарообороты отделений
        /// </summary>
        public IReadOnlyList<TurnoverNormative> TurnoverNormatives { get; }

        /// <summary>
        /// Группы отделений торговой сети
        /// </summary>
        public IReadOnlyList<DepartmentsGroup> DepartmentsGroups { get; }

        /// <summary>
        /// Коэффициент новых отделений
        /// </summary>
        public NewDepartmentsCoefficient NewDepartmentsCoefficient { get; }

        /// <summary>
        /// Команда расчета планируемого товарооборота по указанным данным
        /// </summary>
        public DelegateCommand CalculateTurnoverCommand { get; }

        /// <summary>
        /// Команда сохранения модели в файл
        /// </summary>
        public DelegateCommand SaveModelCommand { get; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="dialogService">Средство показа диалоговых окон</param>
        /// <param name="serializeStream">Средство записи объектов в файл, их чтения из файла</param>
        /// <param name="model">Модель расчета планируемого товарооборота</param>
        public ModelEditingViewModel
            (IDialogService dialogService,
            ISerializeStream serializeStream,
            TurnoverModel model)
        {
            Requires.NotNull(model, nameof(model));
            Requires.NotNull(dialogService, nameof(dialogService));
            Requires.NotNull(serializeStream, nameof(serializeStream));

            _dialogService = dialogService;
            _serializeStream = serializeStream;
            _model = model;

            LikeForLikes = model.LikeForLikes;
            TurnoverNormatives = model.TurnoverNormatives;
            DepartmentsGroups = model.DepartmentsGroups;
            NewDepartmentsCoefficient = model.NewDepartmentsCoefficient;
            CalculateTurnoverCommand = new DelegateCommand(model.CalculateTurnover);
            SaveModelCommand = new DelegateCommand(SaveModel);
        }

        /// <summary>
        /// Сохранение модели прогнозирования товарооборота в указанный в диалоговом окне файл
        /// </summary>
        private void SaveModel()
        {
            try
            {
                if (_dialogService.SaveFileDialog(out string path))
                {
                    _serializeStream.Write(path, new[] { _model });
                    _dialogService.MessageDialog("Успех", "Модель товарооборота успешно сохранена");
                }
            }
            catch
            {
                _dialogService.MessageDialog("Ошибка", "В ходе сохранения модели товарооборота произошла ошибка");
            }
        }
    }
}