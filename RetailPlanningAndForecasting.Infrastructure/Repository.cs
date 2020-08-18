using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using CodeContracts;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Infrastructure
{
    /// <summary>
    /// Функционал манипулирования хранилищем сущностеЙ:
    /// - выборка сущностей из хранилища
    /// - добавление сущности в хранилище
    /// - удаление сущности из хранилища
    /// </summary>
    /// <typeparam name="T">Тип хранимой сущности</typeparam>
    public sealed class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Набор сущностей указанного типа, представленных в базе данных
        /// </summary>
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="dbContext">Контекст базы данных</param>
        public Repository(DbContext dbContext)
        {
            Requires.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Добавление сущности в хранилище
        /// </summary>
        /// <param name="items">Добавляемые сущности</param>
        public void Add(IReadOnlyList<T> items)
        {
            Requires.NotNull(items, nameof(items));

            _dbSet.AddRange(items.ToArray());
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Удаление сущностей из хранилища
        /// </summary>
        /// <param name="items">Сущности для удаления</param>
        public void Delete(IReadOnlyList<T> items)
        {
            Requires.NotNull(items, nameof(items));

            _dbSet.RemoveRange(items);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Получение всех сущностей, содержащихся в хранилище
        /// </summary>
        /// <returns>Сущности, представленные в хранилище</returns>
        public List<T> Get() =>
            _dbSet.ToList();

        /// <summary>
        /// Получение сущностей хранилища, для которых выполняется заданный предикат
        /// </summary>
        /// <param name="predicate">Предикат, по которому происходит отбор сущностейы</param>
        /// <returns>Сущности, удовлетворяющие условию выборки</returns>
        public List<T> Get(Predicate<T> predicate)
        {
            Requires.NotNull(predicate, nameof(predicate));

            return _dbSet.Where(item => predicate(item)).ToList();
        }

        /// <summary>
        /// Очистка хранилища: удаление всех сущностей из хранилища
        /// </summary>
        public void Clear()
        {
            _dbSet.RemoveRange(_dbSet);
            _dbContext.SaveChanges();
        }
    }
}