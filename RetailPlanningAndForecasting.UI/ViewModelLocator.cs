using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Infrastructure;
using RetailPlanningAndForecasting.Presentation;
using GalaSoft.MvvmLight.Views;
using Unity;
using CodeContracts;
using System;
using MahApps.Metro.Controls;

namespace RetailPlanningAndForecasting.UI
{
    public static class ViewModelLocator
    {
        private static IUnityContainer container;

        public static void Initalize(MainWindow window)
        {
            Requires.NotNull(window, nameof(window));
            container = new UnityContainer()
                .RegisterSingleton<AppDbContext>()
                .RegisterSingleton<IRepositoryCreator, RepositoryCreator>()
                .RegisterInstance(typeof(MetroWindow), window)
                .RegisterSingleton<IDialogService, DialogService>()
                .RegisterSingleton<SettingsViewModel>();
        }

        public static SettingsViewModel SettingsViewModel
        {
            get
            {
                if (container == null)
                    throw new InvalidOperationException("The ViewModelLocator initializer must be called before getting the ViewModel instance");
                return container.Resolve<SettingsViewModel>();
            }
        }
    }
}