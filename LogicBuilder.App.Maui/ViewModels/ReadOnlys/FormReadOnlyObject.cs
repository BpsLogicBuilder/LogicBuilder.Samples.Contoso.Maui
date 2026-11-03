using LogicBuilder.App.Maui.Directives;
using LogicBuilder.App.Maui.Directives.Factories;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.Views.Factories;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace LogicBuilder.App.Maui.ViewModels.ReadOnlys
{
    public partial class FormReadOnlyObject<T> : ReadOnlyObjectBase<T>, IDisposable where T : class
    {
        public FormReadOnlyObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IPopupFormFactory popupFormFactory,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IUiNotificationService uiNotificationService,
            string name,
            IChildFormGroupSettings setting) 
            : base(name, setting.FormGroupTemplate.TemplateName, uiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.popupFormFactory = popupFormFactory;
            this.propertiesUpdater = readOnlyPropertiesUpdater;
            this.Placeholder = this.FormSettings.Placeholder;
            FormLayout = collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                typeof(T),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                null,
                null
            ).CreateFields();

            this.directiveManagers = (ReadOnlyDirectiveManagers<T>)directiveManagersFactory.GetReadOnlyDirectiveManagers
            (
                typeof(T),
                FormLayout.Properties,
                FormSettings
            );
        }

        public IChildFormGroupSettings FormSettings { get; set; }
        private readonly IPopupFormFactory popupFormFactory;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly ReadOnlyDirectiveManagers<T> directiveManagers;

        public DetailFormLayout FormLayout { get; set; }

        public string DisplayText => string.Empty;

        private string _title;
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

        private string _placeholder;
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
                this.propertiesUpdater.UpdateProperties
                (
                    FormLayout.Properties,
                    base.Value,
                    FormSettings.FieldSettings
                );
            }
        }
#pragma warning restore S4275 //using base.Value

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
                            () => Application.Current!.Windows[0].Page!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                            (
                                popupFormFactory.CreateReadOnlyChildFormPage(this)
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
                () => Application.Current!.Windows[0].Page!.Navigation.PopModalAsync()
            );
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed) return;
            if (disposing)
            {
                DisposeComponent(this.directiveManagers);
                foreach (var property in FormLayout.Properties)
                {
                    if (property is IDisposable disposable)
                        DisposeComponent(disposable);
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected static void DisposeComponent(IDisposable disposable)
        {
            disposable?.Dispose();
        }
    }
}
