using RetailPlanningAndForecasting;

namespace RetailPlanningAndForecasting.Services
{
    public interface IRepositoryCreator
    {
        IRepository<T> Create<T>() where T : class;
    }
}