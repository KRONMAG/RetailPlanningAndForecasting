using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class DepartmentsLabel
    {
        [Key]
        public string Name { get; private set; }

        public bool AreDepartmentsNew { get; private set; }

        public DepartmentsLabel(string name, bool areDepartmentsNew)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
            this.AreDepartmentsNew = areDepartmentsNew;
        }

        private DepartmentsLabel()
        {

        }
    }
}