using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using GalaSoft.MvvmLight.Views;
using MahApps.Metro.Controls;

namespace RetailPlanningAndForecasting.UI
{
    public class DialogService : IDialogService
    {
        private MetroWindow _window;

        public DialogService(MetroWindow window) =>
            _window = window;

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback) =>
            ShowMessage(message, title, buttonText, afterHideCallback);

        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback) =>
            await ShowMessage(error.Message, title, "OK", afterHideCallback);

        public async Task ShowMessage(string message, string title) =>
            await ShowMessage(message, title, "OK", delegate { });

        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback) =>
            await _window.ShowMessageAsync
            (
                title,
                message,
                settings: new MetroDialogSettings()
                {
                    AffirmativeButtonText = buttonText
                }
            ).ContinueWith(_ => afterHideCallback());

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback) =>
            await _window.ShowMessageAsync
            (
                title,
                message,
                MessageDialogStyle.AffirmativeAndNegative,
                new MetroDialogSettings()
                {
                    AffirmativeButtonText = buttonConfirmText,
                    NegativeButtonText = buttonCancelText
                }
            ).ContinueWith(result =>
            {
                var isAffirmative = result.Result == MessageDialogResult.Affirmative;
                afterHideCallback(isAffirmative);
                return isAffirmative;
            });

        public Task ShowMessageBox(string message, string title) =>
            ShowMessage(message, title, "OK", delegate { });
            
    }
}