using System;
using MahApps.Metro.Controls;
using GalaSoft.MvvmLight.Views;
using CodeContracts;
using Unity;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Infrastructure;
using RetailPlanningAndForecasting.Presentation;

namespace RetailPlanningAndForecasting.UI
{
    public static class ViewModelLocator
    {
        private const string GET_VIEW_MODEL_ERROR_MESSAGE =
            "The ViewModelLocator initializer must be called before getting the ViewModel instance";

        private static IUnityContainer container;

        public static void Initalize(MainWindow window)
        {
            Requires.NotNull(window, nameof(window));
            container = new UnityContainer()
                .RegisterSingleton<AppDbContext>()
                .RegisterSingleton<IRepositoryCreator, RepositoryCreator>()
                .RegisterInstance(typeof(MetroWindow), window)
                .RegisterSingleton<IDialogService, DialogService>()
                .RegisterSingleton<DepartmentsDirectionsViewModel>()
                .RegisterSingleton<RegionsViewModel>()
                .RegisterSingleton<DepartmentsLabelsViewModel>();
        }

        public static DepartmentsDirectionsViewModel DepartmentsDirectionsViewModel
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException(GET_VIEW_MODEL_ERROR_MESSAGE);
                return container.Resolve<DepartmentsDirectionsViewModel>();
            }
        }

        public static RegionsViewModel RegionsViewModel
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException(GET_VIEW_MODEL_ERROR_MESSAGE);
                return container.Resolve<RegionsViewModel>();
            }
        }

        public static DepartmentsLabelsViewModel DepartmentsLabelsViewModel
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException(GET_VIEW_MODEL_ERROR_MESSAGE);
                return container.Resolve<DepartmentsLabelsViewModel>();
            }
        }
    }
}