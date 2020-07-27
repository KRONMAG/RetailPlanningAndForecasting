﻿using Unity;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Infrastructure;
using RetailPlanningAndForecasting.Presentation;

namespace RetailPlanningAndForecasting.UI
{
    public class ViewModelLocator
    {
        private static IUnityContainer _container;

        static ViewModelLocator() =>
            _container = new UnityContainer()
                .RegisterSingleton<AppDbContext>()
                .RegisterSingleton<IRepositoryCreator, RepositoryCreator>()
                .RegisterSingleton<DepartmentsDirectionsViewModel>()
                .RegisterSingleton<RegionsViewModel>()
                .RegisterSingleton<DepartmentsLabelsViewModel>()
                .RegisterType<CreationModelViewModel>();

        public DepartmentsDirectionsViewModel DepartmentsDirectionsViewModel =>
            _container.Resolve<DepartmentsDirectionsViewModel>();

        public RegionsViewModel RegionsViewModel =>
            _container.Resolve<RegionsViewModel>();

        public DepartmentsLabelsViewModel DepartmentsLabelsViewModel =>
            _container.Resolve<DepartmentsLabelsViewModel>();

        public PlanningPeriodViewModel PlanningPeriodViewModel =>
            _container.Resolve<PlanningPeriodViewModel>();

        public CreationModelViewModel CreationModelViewModel =>
            _container.Resolve<CreationModelViewModel>();
    }
}