using System.Linq;
using System.Collections.Generic;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class TurnoverCalculator
    {
        private readonly IReadOnlyList<LikeForLike> _likeForLikes;
        private readonly IReadOnlyList<TurnoverNormative> _turnoverNormatives;
        private readonly NewDepartmentsCoefficient _newDepartmentsCoefficient;

        public TurnoverCalculator(
            IReadOnlyList<LikeForLike> likeForLikes,
            IReadOnlyList<TurnoverNormative> normatives,
            NewDepartmentsCoefficient coefficient)
        {
            Requires.NotNull(likeForLikes, nameof(likeForLikes));
            Requires.NotNull(normatives, nameof(normatives));
            Requires.NotNull(coefficient, nameof(coefficient));

            this._likeForLikes = likeForLikes;
            this._turnoverNormatives = normatives;
            this._newDepartmentsCoefficient = coefficient;
        }

        public void Calculate(IReadOnlyList<DepartmentsGroup> groups)
        {
            Requires.NotNull(groups, nameof(groups));

            foreach (var group in groups)
                group.PlannedTurnover = null;

            foreach (var group in groups)
            {
                if (group.DepartmentsCount == null)
                    continue;
                var normativeTurnover = _turnoverNormatives
                    .FirstOrDefault(item =>
                        item.DepartmentsLabel.Name == group.DepartmentsLabel.Name &&
                        item.DepartmentDirection.Name == group.DepartmentsDirection.Name &&
                        item.Region.Name == group.Region.Name);
                if (normativeTurnover?.NormativeTurnover == null)
                    continue;
                if (group.DepartmentsLabel.AreDepartmentsNew)
                {
                    if (_newDepartmentsCoefficient.Value == null)
                        continue;
                    group.PlannedTurnover = group.DepartmentsCount *
                        normativeTurnover.NormativeTurnover *
                        _newDepartmentsCoefficient.Value;
                }
                else
                {
                    var likeForLike = _likeForLikes
                        .FirstOrDefault(item =>
                            item.DepartmentsLabel.Name == group.DepartmentsLabel.Name &&
                            item.Year == group.Year);
                    if (likeForLike?.Coefficient == null)
                        continue;
                    group.PlannedTurnover = group.DepartmentsCount *
                        normativeTurnover.NormativeTurnover *
                        likeForLike.Coefficient;
                }
            }
        }
    }
}