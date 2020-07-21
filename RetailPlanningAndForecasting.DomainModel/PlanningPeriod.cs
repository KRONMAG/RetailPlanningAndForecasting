using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class PlanningPeriod
    {
        [Key, Column(Order = 1)]
        public int StartYear { get; private set; }

        [Key, Column(Order = 2)]
        public int EndYear { get; private set; }

        public PlanningPeriod(int startYear, int endYear)
        {
            Requires.InRange(startYear > 0, nameof(startYear));
            Requires.InRange(endYear > 0, nameof(endYear));
            Requires.True(startYear <= endYear, "Год начала периода не может быть больше года его конца");

            StartYear = startYear;
            EndYear = endYear;
        }

        private PlanningPeriod()
        {

        }
    }
}
