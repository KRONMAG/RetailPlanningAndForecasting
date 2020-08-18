using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Период планирования товарооборота
    /// </summary>
    public sealed class PlanningPeriod
    {
        /// <summary>
        /// Год начала периода
        /// </summary>
        [Key, Column(Order = 1)]
        public int StartYear { get; private set; }

        /// <summary>
        /// Год окончания периода
        /// </summary>
        [Key, Column(Order = 2)]
        public int EndYear { get; private set; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="startYear">Год начала периода</param>
        /// <param name="endYear">Год окончания периода</param>
        public PlanningPeriod(int startYear, int endYear)
        {
            Requires.InRange(startYear > 0, nameof(startYear));
            Requires.InRange(endYear > 0, nameof(endYear));
            Requires.True(startYear <= endYear, "Год начала периода не может быть больше года его конца");

            StartYear = startYear;
            EndYear = endYear;
        }

        /// <summary>
        /// Конструктор для десериализации объекта
        /// </summary>
        private PlanningPeriod()
        {

        }
    }
}
