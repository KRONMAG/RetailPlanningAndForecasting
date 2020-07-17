using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    public class Region
    {
        [Key]
        public string Name { get; private set; }

        public Region(string name)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
        }

        private Region()
        {

        }
    }
}