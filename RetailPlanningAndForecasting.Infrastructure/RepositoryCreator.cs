using RetailPlanningAndForecasting.Services;
using CodeContracts;
using System;

namespace RetailPlanningAndForecasting.Infrastructure
{
    public class RepositoryCreator : IRepositoryCreator
    {
        public AppDbContext _dbContext;

        public RepositoryCreator(AppDbContext dbContext)
        {
            Requires.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public IRepository<T> Create<T>() where T : class
        {
            try
            {
                return new Repository<T>(_dbContext);
            }
            catch(InvalidOperationException e)
            {
                throw new InvalidOperationException($"Cannot create repository: unknown entity type {typeof(T).Name}", e);
            }
        }
    }
}