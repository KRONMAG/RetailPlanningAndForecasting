using System.IO;
using System.Linq;
using System.Collections.Generic;
using CodeContracts;
using Newtonsoft.Json;
using JsonNet.PrivateSettersContractResolvers;
using RetailPlanningAndForecasting.Services;

namespace RetailPlanningAndForecasting.Infrastructure
{
    /// <summary>
    /// Реализация функционала:
    /// - сериализации и сохранения объектов в файл
    /// - загрузки объектов из файла и их десереализации
    /// </summary>
    public class SerializeStream : ISerializeStream
    {
        /// <summary>
        /// Сериализация объектов, запись полученных данных в файл
        /// </summary>
        /// <typeparam name="T">Тип записываемых объектов</typeparam>
        /// <param name="path">Путь к файлу, в который будут записаны данные</param>
        /// <param name="data">Список объектов для записи</param>
        public void Write<T>(string path, IReadOnlyList<T> data)
        {
            Requires.NotNullOrEmpty(path, nameof(path));
            Requires.NotNull(data, nameof(data));

            File.WriteAllText
            (
                path,
                JsonConvert.SerializeObject
                (
                    data,
                    new JsonSerializerSettings()
                    {
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        ContractResolver = new PrivateSetterContractResolver()
                    }
                )
            );
        }

        /// <summary>
        /// Загрузка данных из файла, десериализация объектов
        /// </summary>
        /// <typeparam name="T">Тип читаемых объектов</typeparam>
        /// <param name="path">Путь к файлу с данными</param>
        /// <returns>Прочитанные десериализованные объекты</returns>
        public List<T> Read<T>(string path)
        {
            Requires.NotNullOrEmpty(path, nameof(path));
            Requires.True(File.Exists(path), nameof(path), $"Не найден файл {path}");

            return JsonConvert
                .DeserializeObject<IReadOnlyList<T>>(File.ReadAllText(path))
                .ToList();
        }
    }
}