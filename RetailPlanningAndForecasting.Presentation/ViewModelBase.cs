using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;

namespace RetailPlanningAndForecasting.Presentation
{
    /// <summary>
    /// Базовый класс модели представления, реализующий механизмы уведомления клиента:
    /// - об изменении значений свойств модели представления;
    /// - о возникновении ошибок при попытке задания свойствам недопустимых значений
    /// </summary>
    public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Хранилище отрицательных результатов валидации свойств
        /// </summary>
        private ErrorsContainer<ValidationResult> _errorsContainer;

        /// <summary>
        /// Событие изменения ошибок проверки свойства
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Содержит ли объект в данный момент ошибки валидации свойств
        /// </summary>
        public bool HasErrors => this._errorsContainer.HasErrors;

        /// <summary>
        /// Создание экземпляра модели представления
        /// </summary>
        public ViewModelBase() =>
            _errorsContainer = new ErrorsContainer<ValidationResult>(propertyName =>
                this.RaiseErrorsChanged(propertyName));

        /// <summary>
        /// Получение ошибок валидации указанного свойства,
        /// </summary>
        /// <param name="propertyName">Наименование свойства</param>
        /// <returns>Ошибки проверки заданного свойства</returns>
        public IEnumerable GetErrors(string propertyName) =>
            this._errorsContainer.GetErrors(propertyName);

        /// <summary>
        /// Генерация события изменения ошибок проверки указанного свойства
        /// </summary>
        /// <param name="propertyName">Наименование свойства</param>
        protected void RaiseErrorsChanged(string propertyName) =>
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        /// <summary>
        /// Добавление ошибки проверки указанного свойства в хранилище
        /// </summary>
        /// <param name="propertyName">Наименование свойства</param>
        /// <param name="message">Сообщение с описанием возникшей ошибки</param>
        protected void AddError(string propertyName, string message) =>
            _errorsContainer.SetErrors(propertyName, new[] { new ValidationResult(message) });

        /// <summary>
        /// Удаление ошибок валидации из хранилища для указанного свойства
        /// </summary>
        /// <param name="propertyName">Наименование свойства</param>
        protected void ClearErrors(string propertyName) =>
            _errorsContainer.ClearErrors(propertyName);
    }
}