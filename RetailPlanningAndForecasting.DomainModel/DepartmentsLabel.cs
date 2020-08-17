using System;
using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Метка отделений торговой сети
    /// </summary>
    [Serializable]
    public class DepartmentsLabel
    {
        /// <summary>
        /// Наименование метки
        /// </summary>
        [Key]
        public string Name { get; private set; }

        /// <summary>
        /// Стоит ли рассчитывать планируемый товарооборот для
        /// отделений с данной меткой по формуле для новых отделений
        /// </summary>
        public bool AreDepartmentsNew { get; private set; }

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="name">Наименование метки</param>
        /// <param name="areDepartmentsNew">
        /// Стоит ли рассчитывать планируемый товарооборот для
        /// отделений с данной меткой по формуле для новых отделений
        /// </param>
        public DepartmentsLabel(string name, bool areDepartmentsNew)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
            this.AreDepartmentsNew = areDepartmentsNew;
        }

        /// <summary>
        /// Конструктор для десериализации объекта
        /// </summary>
        private DepartmentsLabel()
        {

        }
    }
}