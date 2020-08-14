using System;
using System.Collections.Generic;

namespace RetailPlanningAndForecasting.Services
{
    /// <summary>
    /// Описание функционала манипулирования хранилищем сущности указанного типа 
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получение всех сущностей, содержащихся в хранилище
        /// </summary>
        /// <returns>Сущности, представленные в хранилище</returns>
        List<T> Get();

        /// <summary>
        /// Получение сущностей, удовлетворяющих предикату
        /// </summary>
        /// <param name="predicate">Предикат для выборки сущностей</param>
        /// <returns>Сущности с истинным условием выборки</returns>
        List<T> Get(Predicate<T> predicate);

        /// <summary>
        /// Добавление сущностей в хранилище
        /// </summary>
        /// <param name="items">Добавляемые сущности</param>
        void Add(IReadOnlyList<T> items);

        /// <summary>
        /// Удаление сущностей из хранилища
        /// </summary>
        /// <param name="items">Удаляемые сущности</param>
        void Delete(IReadOnlyList<T> items);

        /// <summary>
        /// Очистка хранилища сущностей
        /// </summary>
        void Clear();
    }
}