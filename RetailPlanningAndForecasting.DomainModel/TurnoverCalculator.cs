﻿using System.Linq;
using System.Collections.Generic;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class TurnoverCalculator
    {
        private IReadOnlyList<LikeForLike> _likeForLikes;
        private IReadOnlyList<NormativeTurnover> _normativeTurnovers;
        private NewDepartmentsCoefficient _newDepartmentsCoefficient;

        public void Calculate(DepartmentsGroup group)
        {
            foreach (var row in group)
                row.Turnover = null;
            if (group.DepartmentsType == DepartmentsType.New && _newDepartmentsCoefficient.Value == null)
                return;
            foreach (var row in group)
            {
                if (row.DepartmentsCount == null)
                    continue;
                var likeForLike = _likeForLikes
                    .FirstOrDefault(item =>
                        item.DepartmentType == group.DepartmentsType &&
                        item.Year == row.Year);
                var normativeTurnover = _normativeTurnovers
                    .FirstOrDefault(item =>
                        item.DepartmentDirection == group.DepartmentsDirection &&
                        item.Region == group.Region);
                if (likeForLike?.Coefficient != null && normativeTurnover != null)
                {
                    if (group.DepartmentsType == DepartmentsType.Old && normativeTurnover.OldDepartmentTurnover != null)
                    {
                        row.Turnover = row.DepartmentsCount *
                            normativeTurnover.OldDepartmentTurnover *
                            likeForLike.Coefficient;
                    }
                    else if (group.DepartmentsType == DepartmentsType.New && normativeTurnover.NewDepartmentTurnover != null)
                    {
                        row.Turnover = row.DepartmentsCount *
                            normativeTurnover.NewDepartmentTurnover *
                            likeForLike.Coefficient *
                            _newDepartmentsCoefficient.Value;
                    }
                }
            }
        }

        public TurnoverCalculator(
            IReadOnlyList<LikeForLike> likeForLikes,
            IReadOnlyList<NormativeTurnover> normativeTurnovers,
            NewDepartmentsCoefficient newDepartmentsCoefficient)
        {
            Requires.NotNull(likeForLikes, nameof(likeForLikes));
            Requires.NotNull(normativeTurnovers, nameof(normativeTurnovers));
            Requires.NotNull(newDepartmentsCoefficient, nameof(newDepartmentsCoefficient));

            this._likeForLikes = likeForLikes;
            this._normativeTurnovers = normativeTurnovers;
            this._newDepartmentsCoefficient = newDepartmentsCoefficient;
        }
    }
}