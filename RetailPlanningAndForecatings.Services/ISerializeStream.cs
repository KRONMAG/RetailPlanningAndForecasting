using System.Collections.Generic;

namespace RetailPlanningAndForecasting.Services
{
    public interface ISerializeStream
    {
        void Write<T>(string path, IReadOnlyList<T> items);
        List<T> Read<T>(string path);
    }
}