using System.Linq;
using System.Collections.Generic;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class TurnoverModel
    {
        public IReadOnlyList<LikeForLike> LikeForLikes { get; }

        public IReadOnlyList<TurnoverNormative> TurnoverNormatives { get; }

        public IReadOnlyList<DepartmentsGroup> DepartmentsGroups { get; }

        public NewDepartmentsCoefficient NewDepartmentsCoefficient { get; }

        public TurnoverModel(IReadOnlyList<Region> regions,
            IReadOnlyList<DepartmentsDirection> directions,
            IReadOnlyList<DepartmentsLabel> labels,
            PlanningPeriod period)
        {
            Requires.NotNull(regions, nameof(regions));
            Requires.NotNull(directions, nameof(directions));
            Requires.NotNull(labels, nameof(labels));
            Requires.NotNull(period, nameof(period));

            var years = Enumerable.Range(period.StartYear, period.EndYear - period.StartYear + 1);
            var product = (from region in regions
                           from direction in directions
                           from label in labels
                           from year in years
                           select (region, direction, label, year));

            LikeForLikes = product
                .Select(item => (item.label, item.year))
                .Distinct()
                .Select(item => new LikeForLike(item.label, item.year))
                .ToList();

            TurnoverNormatives = product
                .Select(item => (item.region, item.direction, item.label))
                .Distinct()
                .Select(item => new TurnoverNormative(item.region, item.direction, item.label))
                .ToList();

            DepartmentsGroups = product
                .Select(item =>
                    new DepartmentsGroup(item.region, item.direction, item.label, item.year))
                .ToList();

            NewDepartmentsCoefficient = new NewDepartmentsCoefficient();
        }

        public void CalculateTurnover()
        {
            foreach (var group in DepartmentsGroups)
                group.PlannedTurnover = null;

            foreach (var group in DepartmentsGroups)
            {
                if (group.DepartmentsCount == null)
                    continue;
                var normativeTurnover = TurnoverNormatives
                    .First(item =>
                        item.DepartmentsLabel.Name == group.DepartmentsLabel.Name &&
                        item.DepartmentsDirection.Name == group.DepartmentsDirection.Name &&
                        item.Region.Name == group.Region.Name);
                if (normativeTurnover.NormativeTurnover == null)
                    continue;
                if (group.DepartmentsLabel.AreDepartmentsNew)
                {
                    if (NewDepartmentsCoefficient.Value == null)
                        continue;
                    group.PlannedTurnover = group.DepartmentsCount *
                        normativeTurnover.NormativeTurnover *
                        NewDepartmentsCoefficient.Value;
                }
                else
                {
                    var likeForLike = LikeForLikes
                        .First(item =>
                            item.DepartmentsLabel.Name == group.DepartmentsLabel.Name &&
                            item.Year == group.Year);
                    if (likeForLike.Coefficient == null)
                        continue;
                    group.PlannedTurnover = group.DepartmentsCount *
                        normativeTurnover.NormativeTurnover *
                        likeForLike.Coefficient;
                }
            }
        }
    }
}