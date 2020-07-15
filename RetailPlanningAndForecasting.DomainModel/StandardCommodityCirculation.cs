namespace RetailPlanningAndForecasting.DomainModel
{
    public class AverageTurnover
    {
        public DepartmentDirection DepartmentDirection { get; }

        public Region Region { get; }

        public decimal? OldDepartmentTurnover { get; }

        public decimal? NewDepartmentTurnover { get; }
    }
}