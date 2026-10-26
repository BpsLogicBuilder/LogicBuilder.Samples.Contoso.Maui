using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Views.Factories;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public abstract partial class BaseFormValidatableObject<T>(string name, string templateName, IEnumerable<IValidationRule>? validations, IUiNotificationService uiNotificationService) : ValidatableObjectBase<T>(name, templateName, validations, uiNotificationService), IDisposable where T : class
    {
        public event EventHandler? Cancelled;
        public event EventHandler? Submitted;

        public abstract EditFormLayout FormLayout { get; set; }

        public abstract IChildFormGroupSettings FormSettings { get; set; }
        protected abstract IEntityUpdater EntityUpdater { get; }
        protected abstract IPropertiesUpdater PropertiesUpdater { get; }
        protected abstract IDisposable PropertyChangedSubscription { get; }
        internal abstract DirectiveManagers<T> DirectiveManagers { get; }

        protected abstract IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        protected abstract IPopupFormFactory PopupFormFactory { get; }
        private string _title = "";
        public string Title
        {
            get => _title;
            [MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        public string DisplayText => string.Empty;

        private string _placeholder = "";
        public string Placeholder
        {
            get => _placeholder;
            [MemberNotNull(nameof(_placeholder))]
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

#pragma warning disable S4275 //using base.Value
        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                this.PropertiesUpdater.UpdateProperties
                (
                    FormLayout.Properties,
                    base.Value,
                    FormSettings.FieldSettings
                );

                IsValid = Validate();

                OnPropertyChanged(nameof(DisplayText));
            }
        }
#pragma warning restore S4275 //using base.Value

        public ICommand TextChangedCommand => new Command
        (
            (parameter) =>
            {
                IsDirty = true;
                string text = ((TextChangedEventArgs)parameter).NewTextValue;
                if (text == null)
                    return;

                IsValid = Validate();
            }
        );

        private ICommand? _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand != null)
                    return _submitCommand;

                _submitCommand = new Command
                (
                    () =>
                    {
                        Value = this.EntityUpdater.ToModelObject
                        (
                            FormLayout.Properties,
                            this.FormSettings.FieldSettings,
                            Value
                        );

                        Placeholder = IsValid ? this.FormSettings.ValidFormControlText : this.FormSettings.InvalidFormControlText;

                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.Windows[0].Page!.Navigation.PopModalAsync()
                        );

                        Submitted?.Invoke(this, new EventArgs());
                    },
                    canExecute: AreFieldsValid
                );

                return _submitCommand;
            }
        }

        private ICommand? _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand != null)
                    return _openCommand;

                _openCommand = new Command
                (
                    () =>
                    {
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => App.Current!.Windows[0].Page!.Navigation.PushModalAsync/*App.Current.MainPage is not null here*/
                            (
                                PopupFormFactory.CreateChildFormPage(this)
                            )
                        );
                    }
                );

                return _openCommand;
            }
        }

        private ICommand? _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand != null)
                    return _cancelCommand;

                _cancelCommand = new Command
                (
                    Cancel
                );

                return _cancelCommand;
            }
        }

        protected virtual void Cancel()
        {
            MainThread.BeginInvokeOnMainThread
            (
                () => App.Current!.Windows[0].Page!.Navigation.PopModalAsync()
            );

            Cancelled?.Invoke(this, new EventArgs());
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                RemoveDisposables(this.DirectiveManagers);
                RemoveDisposables(this.PropertyChangedSubscription);
                foreach (var property in FormLayout.Properties)
                {
                    if (property is IDisposable disposable)
                        RemoveDisposables(disposable);
                }
            }

            this.disposed = true;
        }

        protected static void RemoveDisposables(IDisposable disposable)
        {
            disposable?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override bool Validate()
        {
            if (!AreFieldsValid())
                Errors = new Dictionary<string, string> { [Name] = this.FormSettings.InvalidFormControlText };

            IsValid = Errors == null || Errors.Count == 0;

            return this.IsValid;
        }

        private bool AreFieldsValid()
            => FormLayout.Properties.Aggregate
            (
                true,
                (isTrue, next) => next.Validate() && isTrue
            );

        protected void FieldChanged(string fieldName)
        {
            ((Command)SubmitCommand).ChangeCanExecute();
        }

    }
}
