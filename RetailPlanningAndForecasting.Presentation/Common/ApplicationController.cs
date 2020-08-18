using System;
using System.Collections.Generic;
using Unity;

namespace RetailPlanningAndForecasting.Presentation.Common
{
    /// <summary>
    /// Контроллер приложения, представляющий централизованный контроль
    /// над представлениями, моделями представления и их зависимостями
    /// </summary>
    public sealed class ApplicationController
    {
        /// <summary>
        /// Словарь, сопоставляющий определенному классу модели представления
        /// класс представления
        /// </summary>
        private readonly IDictionary<Type, Type> _viewModelsAndViews;

        /// <summary>
        /// Контейнер инверсии управления
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// Создание контроллера приложения
        /// </summary>
        public ApplicationController()
        {
            _container = new UnityContainer();
            _container.RegisterInstance(this);
            _viewModelsAndViews = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Регистрация зависимости в форме экземпляра определенного класса (интерфейса)
        /// </summary>
        /// <typeparam name="T">Класс (интерфейс) зависимости</typeparam>
        /// <param name="instance">Экземлпяр класса (экземпляр класса, реализующего интерфейс)</param>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance<T>(instance);
            return this;
        }

        /// <summary>
        /// Регистрация зависимости в форме одиночки
        /// </summary>
        /// <typeparam name="T">Класс зависимости</typeparam>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterSingleton<T>()
        {
            _container.RegisterSingleton<T>();
            return this;
        }

        /// <summary>
        /// Регистрация зависимости в форме одиночки для интерфейса и класса его реализующего
        /// или базового класса и класса-наследника
        /// </summary>
        /// <typeparam name="T">Интерфейс (родительский класс)</typeparam>
        /// <typeparam name="U">Класс, реализующий интерфейс (класс-наследник)</typeparam>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterSingleton<T, U>() where U : class, T
        {
            _container.RegisterSingleton<T, U>();
            return this;
        }

        /// <summary>
        /// Регистрация зависимости класса
        /// </summary>
        /// <typeparam name="T">Класс зависимости</typeparam>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterType<T>()
        {
            _container.RegisterType<T>();
            return this;
        }

        /// <summary>
        /// Регистрация зависимости в виде интерфейса и реализующего его класса или 
        /// базового класса и класса-наследника
        /// </summary>
        /// <typeparam name="T">Интерфейс (родительский класс)</typeparam>
        /// <typeparam name="U">Класс, реализующий интерфейс (класс-потомок)</typeparam>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterType<T, U>() where U : class, T
        {
            _container.RegisterType<T, U>();
            return this;
        }

        /// <summary>
        /// Регистрация зависимости для модели представления и соответствующего ей представления
        /// </summary>
        /// <typeparam name="T">Класс модели представления</typeparam>
        /// <typeparam name="U">Класс представления</typeparam>
        /// <returns>Контроллер приложения</returns>
        public ApplicationController RegisterViewModel<T, U>() where T : ViewModelBase where U: IView
        {
            _container.RegisterType<T>();
            _container.RegisterType<U>();
            _viewModelsAndViews[typeof(T)] = typeof(U);
            return this;
        }

        /// <summary>
        /// Создание и показ представления, соответствующего указанной модели представления
        /// Экземпляр модели представления передается в конструктор класса предсталвения
        /// </summary>
        /// <typeparam name="T">Класс модели представления</typeparam>
        public void Run<T>() where T : ViewModelBase =>
            ((IView)_container.Resolve(_viewModelsAndViews[typeof(T)])).Show();
    }
}