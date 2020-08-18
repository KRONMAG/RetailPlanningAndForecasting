using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Регион размещения отделений торговой сети
    /// </summary>
    public sealed class Region
    {
        /// <summary>
        /// Наименование региона размещения
        /// </summary>
        [Key]
        public string Name { get; private set; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="name">Наименование региона размещения</param>
        public Region(string name)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Конструктор для десериализации объекта
        /// </summary>
        private Region()
        {

        }
    }
}