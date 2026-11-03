using AutoMapper;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Services;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Utils.Interfaces;
using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public partial class PickerReadOnlyObject<T> : ReadOnlyObjectBase<T>, IHasItemsSourceReadOnly
    {
        public PickerReadOnlyObject(
            IServiceProvider serviceProvider,
            string name,
            string title,
            string stringFormat,
            DropDownTemplateDescriptor dropDownTemplate) : base(name, dropDownTemplate.TemplateName, serviceProvider.GetRequiredService<IUiNotificationService>())
        {
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = serviceProvider.GetRequiredService<IHttpService>();
            _defaultTitle = title;
            _stringFormat = stringFormat;
            this.Title = _defaultTitle;
            this.mapper = serviceProvider.GetRequiredService<IMapper>();
            this.genericsHelpers = serviceProvider.GetRequiredService<IGenericsHelpers>();
            this.serviceProvider = serviceProvider;
            _ = GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly IGenericsHelpers genericsHelpers;
        private readonly IServiceProvider serviceProvider;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private List<object>? _items;
        private readonly IMapper mapper;
        private readonly string _defaultTitle;
        private readonly string _stringFormat;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        public string DisplayText
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_stringFormat))
                    return genericsHelpers.GetPropertyValue<string>(SelectedItem, _dropDownTemplate.TextField) ?? "";

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    _stringFormat,
                    genericsHelpers.GetPropertyValue<string>(SelectedItem, _dropDownTemplate.TextField)
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

        private string? _placeholder;
        public string? Placeholder
        {
            get => _placeholder;
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public object? SelectedItem
        {
            get
            {
                if (_items == null || _items.Count == 0)
                    return null;

                return _items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        genericsHelpers.GetPropertyValue<T>(i, _dropDownTemplate.ValueField)
                    )
                );
            }
        }

#pragma warning disable S4275 //using base.Value
        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }
#pragma warning restore S4275 //using base.Value

        public List<object>? Items
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
            await GetItems(this.DropDownTemplate.TextAndValueSelector);
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(DisplayText));
        }

        private async Task GetItems(SelectorLambdaDescriptor selector)
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._dropDownTemplate.RequestDetails.DataType,
                        ModelType = this._dropDownTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._dropDownTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._dropDownTemplate.RequestDetails.DataReturnType,
                        Selector = selector
                    },
                    this._dropDownTemplate.RequestDetails.DataSourceUrl
                );

                if (!response.Success)
                {
#if DEBUG
                    await Application.Current!.Windows[0].Page!.DisplayAlertAsync/*App.Current.MainPage is not null at this point*/
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = [.. ((GetListResponse)response).List.Cast<object>()];
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async void Reload(object entity, Type entityType)
        {
            using (IScopedFlowManagerService flowManagerService = this.serviceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                flowManagerService.SetFlowDataCacheItem
                (
                    entityType.FullName ?? throw new ArgumentException($"{nameof(entityType.FullName)}: {{2C2CB57D-A9D0-40C0-80C5-95E1CDE100E4}}"), 
                    entity
                );

                await flowManagerService.RunFlow
                (
                    new NewFlowRequest
                    {
                        InitialModuleName = this._dropDownTemplate.ReloadItemsFlowName
                    }
                );

                if ((flowManagerService.GetFlowDataCacheItem(FlowDataCacheItemKeys.Get_Selector_Success) ?? false).Equals(false))
                    return;

                SelectorLambdaDescriptor selector = this.mapper.Map<SelectorLambdaDescriptor>
                (
                    flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
                );

                this.Title = this._dropDownTemplate.LoadingIndicatorText;

                await GetItems(selector);
            }

            this.Title = _defaultTitle;

            Value = GetExistingValue() ?? default;

            T? GetExistingValue()
            {
                object? existing = Items?.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        genericsHelpers.GetPropertyValue<T>(i, _dropDownTemplate.ValueField)
                    )
                );

                return existing == null
                    ? default
                    : genericsHelpers.GetPropertyValue<T>(existing, _dropDownTemplate.ValueField);
            }
        }

        public override void Clear()
        {
            Items = null;
            Value = default;
        }
    }
}
