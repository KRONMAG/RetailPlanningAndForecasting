using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class PlanningPeriod
    {
        public int StartYear { get; private set; }

        public int EndYear { get; private set; }

        public PlanningPeriod(int startYear, int endYear)
        {
            Requires.InRange(startYear > 0, nameof(startYear));
            Requires.InRange(endYear > 0, nameof(endYear));
            Requires.True(startYear >= endYear, "Start year cannot be less then end year");

            StartYear = startYear;
            EndYear = endYear;
        }

        private PlanningPeriod()
        {

        }
    }
}
