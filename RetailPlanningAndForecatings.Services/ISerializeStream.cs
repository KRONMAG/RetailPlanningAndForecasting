using System.Collections.Generic;

namespace RetailPlanningAndForecasting.Services
{
    /// <summary>
    /// Реализация функционала:
    /// - сериализации и сохранения объектов в файл
    /// - загрузки объектов из файла и их десереализации
    public interface ISerializeStream
    {
        /// <summary>
        /// Сериализация объектов, запись полученных данных в файл
        /// </summary>
        /// <typeparam name="T">Тип записываемых объектов</typeparam>
        /// <param name="path">Путь к файлу, в который будут записаны данные</param>
        /// <param name="data">Список объектов для записи</param>
        void Write<T>(string path, IReadOnlyList<T> items);

        /// <summary>
        /// Загрузка данных из файла, десериализация объектов
        /// </summary>
        /// <typeparam name="T">Тип читаемых объектов</typeparam>
        /// <param name="path">Путь к файлу с данными</param>
        /// <returns>Прочитанные десериализованные объекты</returns>
        List<T> Read<T>(string path);
    }
}