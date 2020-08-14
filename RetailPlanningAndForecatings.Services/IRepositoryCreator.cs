namespace RetailPlanningAndForecasting.Services
{
    /// <summary>
    /// Описание функционала создателя репозиториев
    /// </summary>
    public interface IRepositoryCreator
    {
        /// <summary>
        /// Создание репозитория для заданного типа сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns>Созданный репозиторий</returns>
        IRepository<T> Create<T>() where T : class;
    }
}