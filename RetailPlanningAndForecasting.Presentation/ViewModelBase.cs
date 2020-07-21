using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Prism.Mvvm;

public abstract class ViewModelBase : BindableBase, INotifyDataErrorInfo
{
    private ErrorsContainer<ValidationResult> _errorsContainer;

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public bool HasErrors => this._errorsContainer.HasErrors;

    public ViewModelBase() =>
        _errorsContainer = new ErrorsContainer<ValidationResult>(propertyName =>
            this.RaiseErrorsChanged(propertyName));

    public IEnumerable GetErrors(string propertyName) =>
        this._errorsContainer.GetErrors(propertyName);

    protected void RaiseErrorsChanged(string propertyName) =>
        this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    protected void AddError(string propertyName, string message) =>
        _errorsContainer.SetErrors(propertyName, new[] { new ValidationResult(message) });

    protected void ClearErrors(string propertyName) =>
        _errorsContainer.ClearErrors(propertyName);
}