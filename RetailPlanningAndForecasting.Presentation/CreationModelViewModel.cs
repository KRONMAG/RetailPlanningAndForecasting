using System.Linq;
using System.Collections.Generic;
using RetailPlanningAndForecasting.DomainModel;
using RetailPlanningAndForecasting.Services;
using Prism.Commands;
using CodeContracts;

namespace RetailPlanningAndForecasting.Presentation
{
    public class CreationModelViewModel : ViewModelBase
    {
        private TurnoverModel _model;

        public IReadOnlyList<LikeForLike> LikeForLikes { get; }

        public IReadOnlyList<TurnoverNormative> TurnoverNormatives { get; }

        public IReadOnlyList<DepartmentsGroup> DepartmentGroups { get; }

        public NewDepartmentsCoefficient NewDepartmentsCoefficient { get; }

        public DelegateCommand CalculateTurnoverCommand { get; }

        public CreationModelViewModel(IRepositoryCreator repositoryCreator)
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
            DepartmentGroups = _model.DepartmentsGroups;
            NewDepartmentsCoefficient = _model.NewDepartmentsCoefficient;
            CalculateTurnoverCommand = new DelegateCommand(_model.CalculateTurnover);
        }

        public void CalculateTurnover() =>
            _model.CalculateTurnover();
    }
}