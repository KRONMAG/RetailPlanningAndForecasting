using System.Collections;
using System.Collections.Generic;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class DepartmentsGroup : IEnumerable<GroupStatisticsRow>
    {
        public virtual DepartmentsDirection DepartmentsDirection { get; }

        public virtual Region Region { get; }

        public DepartmentsType DepartmentsType { get; }

        public virtual IList<GroupStatisticsRow> Statistics { get; }

        public DepartmentsGroup(DepartmentsDirection direction, Region region, DepartmentsType departmentsType)
        {
            Requires.NotNull(direction, nameof(direction));
            Requires.NotNull(region, nameof(region));

            this.DepartmentsDirection = direction;
            this.Region = region;
            this.DepartmentsType = departmentsType;
            this.Statistics = new List<GroupStatisticsRow>();
        }

        public IEnumerator<GroupStatisticsRow> GetEnumerator() =>
            this.Statistics.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.Statistics.GetEnumerator();
    }
}