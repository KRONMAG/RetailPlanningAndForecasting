using System;
using CodeContracts;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Infrastructure
{
    /// <summary>
    /// Создатель репозиториев для сущностей заданного типа
    /// </summary>
    public sealed class RepositoryCreator : IRepositoryCreator
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public AppDbContext _dbContext;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="dbContext">
        /// Контекст базы данных, использующийся в качестве подключения
        /// к источнику данных в создаваемых репозиториях
        /// </param>
        public RepositoryCreator(AppDbContext dbContext)
        {
            Requires.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        /// <summary>
        /// Создание репозитория для оперирования сущностями, представленными в источнике данных
        /// </summary>
        /// <typeparam name="T">Тип хранимой сущности</typeparam>
        /// <returns>Репозиторий сущности указанного типа</returns>
        public IRepository<T> Create<T>() where T : class
        {
            try
            {
                return new Repository<T>(_dbContext);
            }
            catch(InvalidOperationException e)
            {
                throw new InvalidOperationException($"Невозможно создать репозиторий: неизвестный тип сущности {typeof(T).Name}", e);
            }
        }
    }
}