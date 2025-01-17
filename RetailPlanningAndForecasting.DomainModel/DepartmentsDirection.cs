﻿using System.ComponentModel.DataAnnotations;
using CodeContracts;

namespace RetailPlanningAndForecasting.DomainModel
{
    /// <summary>
    /// Направление отделений торговой сети
    /// </summary>
    public class DepartmentsDirection
    {
        /// <summary>
        /// Наименование направления
        /// </summary>
        [Key]
        public string Name { get; private set; }

        /// <summary>
        /// Создание экземпляра объекта
        /// </summary>
        /// <param name="name">Наименование направления</param>
        public DepartmentsDirection(string name)
        {
            Requires.NotNullOrEmpty(name, nameof(name));

            Name = name;
        }

        /// <summary>
        /// Конструктор для десериализации объекта
        /// </summary>
        private DepartmentsDirection()
        {

        }
    }
}