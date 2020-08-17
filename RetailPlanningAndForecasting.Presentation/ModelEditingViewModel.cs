using System.Linq;
using System.Collections.Generic;
using Prism.Commands;
using CodeContracts;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Модель представления редактирования модели планирования товарооборота
    /// </summary>
    public class ModelEditingViewModel : ViewModelBase
    {
        /// <summary>
        /// Модель планирования товарооборота
        /// </summary>
        private TurnoverModel _model;

        private IFileDialog _fileDialog;

        private ISerializeStream _serializeStream;

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

        public DelegateCommand SaveModelCommand { get; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
        public ModelEditingViewModel
            (IRepositoryCreator repositoryCreator,
            IFileDialog fileDialog,
            ISerializeStream serializeStream)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));
            Requires.NotNull(fileDialog, nameof(fileDialog));
            Requires.NotNull(serializeStream, nameof(serializeStream));

            _model = new TurnoverModel
            (
                repositoryCreator.Create<Region>().Get(),
                repositoryCreator.Create<DepartmentsDirection>().Get(),
                repositoryCreator.Create<DepartmentsLabel>().Get(),
                repositoryCreator.Create<PlanningPeriod>().Get().First()
            );
            _fileDialog = fileDialog;
            _serializeStream = serializeStream;

            LikeForLikes = _model.LikeForLikes;
            TurnoverNormatives = _model.TurnoverNormatives;
            DepartmentsGroups = _model.DepartmentsGroups;
            NewDepartmentsCoefficient = _model.NewDepartmentsCoefficient;
            CalculateTurnoverCommand = new DelegateCommand(_model.CalculateTurnover);
            SaveModelCommand = new DelegateCommand(SaveModel);
        }

        private void SaveModel()
        {
            base.ClearErrors(null);
            try
            {
                if (_fileDialog.SaveFileDialog(out string path))
                    _serializeStream.Write(path, new[] { _model });
            }
            catch
            {
                base.AddError(null, "В ходе сохранения модели товарооборота произошла ошибка");
            }
        }
    }
}