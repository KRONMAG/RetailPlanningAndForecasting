using System;
using System.Linq;
using System.Collections.Generic;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Модель расчета планируемого товарооборота
    /// </summary>
    [Serializable]
    public class TurnoverModel
    {
        /// <summary>
        /// LikeForLike-коэффициенты
        /// </summary>
        public IReadOnlyList<LikeForLike> LikeForLikes { get; private set; }

        /// <summary>
        /// Нормативные товарообороты
        /// </summary>
        public IReadOnlyList<TurnoverNormative> TurnoverNormatives { get; private set; }

        /// <summary>
        /// Группы отделений, по который расчитывается планируемый товарооборот
        /// </summary>
        public IReadOnlyList<DepartmentsGroup> DepartmentsGroups { get; private set; }

        /// <summary>
        /// Коэффициент новых магазинов
        /// </summary>
        public NewDepartmentsCoefficient NewDepartmentsCoefficient { get; private set; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="regions">Регионы, в которых находятся отделения</param>
        /// <param name="directions">Направления отделений</param>
        /// <param name="labels">Метки отделений</param>
        /// <param name="period">Период планирования товарооборота</param>
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

        private TurnoverModel()
        {

        }

        /// <summary>
        /// Расчет планируемого товарооборота
        /// </summary>
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