using System;
using System.Collections.Generic;

namespace RetailPlanningAndForecasting.Services
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        List<T> Get(Predicate<T> predicate);
        void Add(IReadOnlyList<T> items);
        void Delete(IReadOnlyList<T> items);
        void Clear();
    }
}