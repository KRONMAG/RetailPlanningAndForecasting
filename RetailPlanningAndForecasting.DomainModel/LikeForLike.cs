﻿using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class LikeForLike
    {
        private decimal? _coefficient;

        public DepartmentsLabel DepartmentsLabel { get; }

        public int Year { get; }

        public decimal? Coefficient
        {
            get => _coefficient;
            set
            {
                Requires.InRange(value == null || value > 0, nameof(value));

                _coefficient = value;
            }
        }

        public LikeForLike(DepartmentsLabel departmentsLabel, int year)
        {
            Requires.NotNull(departmentsLabel, nameof(departmentsLabel));
            Requires.InRange(year >= 0, nameof(year));

            this.DepartmentsLabel = departmentsLabel;
            this.Year = year;
        }
    }
}