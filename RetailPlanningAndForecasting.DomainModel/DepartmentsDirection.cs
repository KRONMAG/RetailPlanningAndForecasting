using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class DepartmentsDirection
    {
        [Key]
        public string Name { get; private set; }

        public DepartmentsDirection(string name)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
        }

        private DepartmentsDirection()
        {

        }
    }
}