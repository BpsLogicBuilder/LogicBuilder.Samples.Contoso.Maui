using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.Views.Factories;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Forms.Configuration;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public partial class MultiSelectReadOnlyObject<T, E> : ReadOnlyObjectBase<T>, IHasItemsSourceReadOnly where T : ObservableCollection<E>
    {
        public MultiSelectReadOnlyObject(
            IHttpService httpService,
            IPopupFormFactory popupFormFactory,
            IUiNotificationService uiNotificationService,
            string name,
            List<string> keyFields,
            string title,
            string stringFormat,
            MultiSelectTemplateDescriptor multiSelectTemplate)
            : base(name, multiSelectTemplate.TemplateName, uiNotificationService)
        {
            this._multiSelectTemplate = multiSelectTemplate;
            this._stringFormat = stringFormat;
            this.httpService = httpService;
            this.popupFormFactory = popupFormFactory;
            this.Title = title;
            this.Placeholder = this._multiSelectTemplate.LoadingIndicatorText;
            itemComparer = new MultiSelectItemComparer<E>(keyFields);
            SelectedItems = [];
            _ = GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly IPopupFormFactory popupFormFactory;
        private readonly string _stringFormat;
        private readonly MultiSelectTemplateDescriptor _multiSelectTemplate;
        private readonly MultiSelectItemComparer<E> itemComparer;

        public MultiSelectTemplateDescriptor MultiSelectTemplate => _multiSelectTemplate;

        public string DisplayText
        {
            get
            {
                if (Value == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(this._stringFormat))
                    return GetText();

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    this._stringFormat,
                    GetText()
                );

                string GetText()
                    => string.Join
                    (
                        ", ",
                        Value.Select
                        (
                            item => typeof(E).GetProperty(_multiSelectTemplate.TextField)?.GetValue(item) ?? string.Empty
                        )
                    );
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

                UpdateSelectedItems();

                OnPropertyChanged(nameof(DisplayText));
                OnPropertyChanged(nameof(SelectedItems));
            }
        }
#pragma warning restore S4275 //using base.Value

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

        public void Reload(object entity, Type entityType)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            Items = null;
            Value = default;
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

            this.Placeholder = this._multiSelectTemplate.PlaceholderText;
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
                            () => Application.Current!.Windows[0].Page!.Navigation.PushModalAsync/*App.Current.MainPage is not null at this point*/
                            (
                                popupFormFactory.CreateReadOnlyMultiSelectPage(this)
                            )
                        );
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
