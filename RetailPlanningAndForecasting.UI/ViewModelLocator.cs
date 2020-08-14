using Unity;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Infrastructure;
using RetailPlanningAndForecasting.Presentation;

namespace RetailPlanningAndForecasting.UI
{
    /// <summary>
    /// Создатель моделей представления
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Контейнер инверсии зависимостей
        /// </summary>
        private static IUnityContainer _container;

        /// <summary>
        /// Внедрение зависимостей
        /// </summary>
        static ViewModelLocator() =>
            _container = new UnityContainer()
                .RegisterSingleton<AppDbContext>()
                .RegisterSingleton<IRepositoryCreator, RepositoryCreator>()
                .RegisterSingleton<DepartmentsDirectionsViewModel>()
                .RegisterSingleton<RegionsViewModel>()
                .RegisterSingleton<DepartmentsLabelsViewModel>()
                .RegisterType<ModelEditingViewModel>();

        /// <summary>
        /// Создание модели представления редактирования списка направлений отделений
        /// </summary>
        public DepartmentsDirectionsViewModel DepartmentsDirectionsViewModel =>
            _container.Resolve<DepartmentsDirectionsViewModel>();

        /// <summary>
        /// Создание модели представления редактирования списка регионов размещения отделений
        /// </summary>
        public RegionsViewModel RegionsViewModel =>
            _container.Resolve<RegionsViewModel>();

        /// <summary>
        /// Создание модели представления редактирования списка меток отделений
        /// </summary>
        public DepartmentsLabelsViewModel DepartmentsLabelsViewModel =>
            _container.Resolve<DepartmentsLabelsViewModel>();

        /// <summary>
        /// Создание модели представления редактирования периода планирования товарооборота
        /// </summary>
        public PlanningPeriodViewModel PlanningPeriodViewModel =>
            _container.Resolve<PlanningPeriodViewModel>();

        /// <summary>
        /// Создание модели представления редактирования модели планирования товарооборота
        /// </summary>
        public ModelEditingViewModel ModelEditingViewModel =>
            _container.Resolve<ModelEditingViewModel>();
    }
}