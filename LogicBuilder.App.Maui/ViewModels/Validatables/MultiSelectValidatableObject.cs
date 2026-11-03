using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.Views.Factories;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public partial class MultiSelectValidatableObject<T, E> : ValidatableObjectBase<T>, IHasItemsSourceValidatable where T : ObservableCollection<E>
    {
        public MultiSelectValidatableObject(
            IHttpService httpService,
            IPopupFormFactory popupFormFactory,
            IUiNotificationService uiNotificationService,
            string name,
            MultiSelectFormControlSettingsDescriptor setting,
            IEnumerable<IValidationRule>? validations)
            : base(name, setting.MultiSelectTemplate.TemplateName, validations, uiNotificationService)
        {
            this._multiSelectFormControlSettingsDescriptor = setting;
            this._multiSelectTemplate = setting.MultiSelectTemplate;
            this.Title = this._multiSelectTemplate.LoadingIndicatorText;
            this.httpService = httpService;
            this.popupFormFactory = popupFormFactory;
            itemComparer = new MultiSelectItemComparer<E>(_multiSelectFormControlSettingsDescriptor.KeyFields);
            SelectedItems = [];
            this.canExecute = false;
            this.Placeholder = this._multiSelectTemplate.LoadingIndicatorText;
            _ = GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly IPopupFormFactory popupFormFactory;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;
        private readonly MultiSelectFormControlSettingsDescriptor _multiSelectFormControlSettingsDescriptor;
        private readonly MultiSelectItemComparer<E> itemComparer;
        private bool canExecute;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

        public string DisplayText
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                return string.Join
                (
                    ", ",
                    Value.Select
                    (
                        item => typeof(E).GetProperty(_multiSelectTemplate.TextField)?.GetValue(item) ?? string.Empty
                    )
                );
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

#pragma warning disable S4275 //using base.Value
        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;

                UpdateSelectedItems();

                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(SelectedItems));
            }
        }
#pragma warning restore S4275 //using base.Value

        /*SelectedItems not being bound on windows https://github.com/dotnet/maui/issues/8435 */
        ObservableCollection<object> _selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                return _selectedItems;
            }
            [MemberNotNull(nameof(_selectedItems))]
            set
            {
                if (_selectedItems != value)
                {
                    _selectedItems = value;
                }
            }
        }

        private List<E>? _items;
        public List<E>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private async Task GetItemSource()
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._multiSelectTemplate.RequestDetails.DataType,
                        ModelType = this._multiSelectTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._multiSelectTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._multiSelectTemplate.RequestDetails.DataReturnType,
                        Selector = this.MultiSelectTemplate.TextAndValueSelector
                    },
                    this._multiSelectTemplate.RequestDetails.DataSourceUrl
                );

                if (!response.Success)
                {
#if DEBUG
                    await Application.Current!.Windows[0].Page!.DisplayAlertAsync
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = [.. ((GetListResponse)response).List.OfType<E>()];
                UpdateSelectedItems();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private void UpdateSelectedItems()
        {
            if (Items == null || Items.Count == 0)
                return;

            var selected = Value?.Any() != true
                ? []
                : Items.Where(i => Value.Contains(i, itemComparer)).Cast<object>();

            SelectedItems.Clear();
            foreach (var item in selected)
                SelectedItems.Add(item);

            canExecute = true;
            this.Title = this._multiSelectFormControlSettingsDescriptor.Title;
            this.Placeholder = this._multiSelectTemplate.PlaceholderText;
        }

        public void Reload(object entity, Type entityType)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            Items = null;
            Value = this._multiSelectFormControlSettingsDescriptor.ValidationSetting?.DefaultValue as T ?? default;
        }

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
                        Value = (T?)new ObservableCollection<E>
                        (
                            Items?.Where(i => SelectedItems.Cast<E>().Contains(i, itemComparer)) ?? []
                        );

                        MainThread.BeginInvokeOnMainThread
                        (
                            () => Application.Current!.Windows[0].Page!.Navigation.PopModalAsync()
                        );
                    },
                    () => canExecute
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
                    async () =>
                    {
                        await MainThread.InvokeOnMainThreadAsync
                        (
                            () => Application.Current!.Windows[0].Page!.Navigation.PushModalAsync
                            (
                                popupFormFactory.CreateMultiSelectPage(this)
                            )
                        );

                        OnPropertyChanged(nameof(SelectedItems));/*needed for iOS*/
                    });

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
                    () =>
                    {
                        MainThread.BeginInvokeOnMainThread
                        (
                            () => Application.Current!.Windows[0].Page!.Navigation.PopModalAsync()/*App.Current.MainPage is not null at this point*/
                        );
                    });

                return _cancelCommand;
            }
        }
    }
}
