using System.Windows;
using RetailPlanningAndForecasting.Services;
using RetailPlanningAndForecasting.Infrastructure;
using RetailPlanningAndForecasting.Presentation;
using RetailPlanningAndForecasting.Presentation.Common;
using RetailPlanningAndForecasting.UI.ModelEditing;
using RetailPlanningAndForecasting.UI.ModelInitialization;

namespace RetailPlanningAndForecasting
{
    /// <summary>
    /// Класс, представляющий текущее приложение
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Инициализация приложения
        /// </summary>
        public App() =>
            new ApplicationController()
                .RegisterSingleton<AppDbContext>()
                .RegisterSingleton<IDialogService, DialogService>()
                .RegisterSingleton<ISerializeStream, SerializeStream>()
                .RegisterSingleton<IRepositoryCreator, RepositoryCreator>()
                .RegisterSingleton<DepartmentsDirectionsViewModel>()
                .RegisterSingleton<RegionsViewModel>()
                .RegisterSingleton<DepartmentsLabelsViewModel>()
                .RegisterViewModel<ModelEditingViewModel, ModelEditingWindow>()
                .RegisterViewModel<ModelInitializationViewModel, MainWindow>()
                .Run<ModelInitializationViewModel>();
    }
}