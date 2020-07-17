using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using CodeContracts;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            Requires.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(IReadOnlyList<T> items)
        {
            Requires.NotNull(items, nameof(items));

            _dbSet.AddRange(items.ToArray());
            _dbContext.SaveChanges();
        }

        public void Delete(IReadOnlyList<T> items)
        {
            Requires.NotNull(items, nameof(items));

            _dbSet.RemoveRange(items);
            _dbContext.SaveChanges();
        }

        public List<T> Get() =>
            _dbSet.AsNoTracking().ToList();

        public List<T> Get(Predicate<T> predicate)
        {
            Requires.NotNull(predicate, nameof(predicate));

            return _dbSet.Where(item => predicate(item)).AsNoTracking().ToList();
        }

        public void Clear()
        {
            _dbSet.RemoveRange(_dbSet);
            _dbContext.SaveChanges();
        }
    }
}