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
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="repositoryCreator">Создатель репозиториев</param>
        public ModelEditingViewModel(IRepositoryCreator repositoryCreator)
        {
            Requires.NotNull(repositoryCreator, nameof(repositoryCreator));

            _model = new TurnoverModel
            (
                repositoryCreator.Create<Region>().Get(),
                repositoryCreator.Create<DepartmentsDirection>().Get(),
                repositoryCreator.Create<DepartmentsLabel>().Get(),
                repositoryCreator.Create<PlanningPeriod>().Get().First()
            );

            LikeForLikes = _model.LikeForLikes;
            TurnoverNormatives = _model.TurnoverNormatives;
            DepartmentsGroups = _model.DepartmentsGroups;
            NewDepartmentsCoefficient = _model.NewDepartmentsCoefficient;
            CalculateTurnoverCommand = new DelegateCommand(_model.CalculateTurnover);
        }
    }
}