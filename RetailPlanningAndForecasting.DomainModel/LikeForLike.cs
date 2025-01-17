﻿using Prism.Mvvm;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// LikeForLike-коэффициент
    /// </summary>
    public sealed class LikeForLike : BindableBase
    {
        /// <summary>
        /// Значение LikeForLike-коэффициента
        /// </summary>
        private decimal? _coefficient;

        /// <summary>
        /// Метка отделений торговой сети, для которых указан LikeForLike-коэффициент
        /// </summary>
        public DepartmentsLabel DepartmentsLabel { get; private set; }

        /// <summary>
        /// Год, для которого указан LikeForLike-коэффициент
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// Значение LikeForLike-коэффициента
        /// </summary>
        public decimal? Coefficient
        {
            get => _coefficient;
            set
            {
                Requires.True(value == null || value > 0, "LFL-коэффициент должен быть больше нуля");

                SetProperty(ref _coefficient, value);
            }
        }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="departmentsLabel">Метка отделений, для которой будет указан LikeForLike-коэффициента</param>
        /// <param name="year">Год актуальности LikeForLike-коэффициент</param>
        public LikeForLike(DepartmentsLabel departmentsLabel, int year)
        {
            Requires.NotNull(departmentsLabel, nameof(departmentsLabel));
            Requires.InRange(year >= 0, nameof(year));

            DepartmentsLabel = departmentsLabel;
            Year = year;
        }

        /// <summary>
        /// Конструктор по умолчанию для десереализации объекта
        /// </summary>
        private LikeForLike()
        {

        }
    }
}