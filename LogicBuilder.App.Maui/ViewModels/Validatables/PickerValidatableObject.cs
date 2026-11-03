using AutoMapper;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Validators;
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogicBuilder.App.Maui.ViewModels.Validatables
{
    public partial class PickerValidatableObject<T> : ValidatableObjectBase<T>, IHasItemsSourceValidatable, IHasDropDownTemplate
    {
        public PickerValidatableObject(
            IServiceProvider serviceProvider,
            string name,
            T defaultValue,
            DropDownTemplateDescriptor dropDownTemplate,
            IEnumerable<IValidationRule>? validations)
            : base(name, dropDownTemplate.TemplateName, validations, serviceProvider.GetRequiredService<IUiNotificationService>())
        {
            this.defaultValue = defaultValue;
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = serviceProvider.GetRequiredService<IHttpService>();
            this.Title = this._dropDownTemplate.LoadingIndicatorText;
            this.mapper = serviceProvider.GetRequiredService<IMapper>();
            this.genericsHelpers = serviceProvider.GetRequiredService<IGenericsHelpers>();
            this.serviceProvider = serviceProvider;
            _ = InitializeAsync();
        }

        private readonly T defaultValue;
        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private readonly IMapper mapper;
        private readonly IGenericsHelpers genericsHelpers;
        private readonly IServiceProvider serviceProvider;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

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

        private object? _selectedItem;
        public object? SelectedItem
        {
            get
            {
                if (Items == null || Items.Count == 0)
                    return null;

                _selectedItem = Items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value,
                        genericsHelpers.GetPropertyValue<T>(i, _dropDownTemplate.ValueField)
                    )
                );

                return _selectedItem;
            }

            set
            {
                if (_selectedItem == null && value == null)
                    return;

                if (_selectedItem != null && _selectedItem.Equals(value))
                    return;

                _selectedItem = value;

                if (_selectedItem != null)
                    Value = genericsHelpers.GetPropertyValue<T>(_selectedItem, _dropDownTemplate.ValueField);

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
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
#pragma warning restore S4275 //using base.Value

        private List<object>? _items;
        public List<object>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                await GetItemSource();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private async Task GetItemSource()
        {
            await GetItems(this.DropDownTemplate.TextAndValueSelector);
            OnPropertyChanged(nameof(SelectedItem));
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
                    await Application.Current!.Windows[0].Page!.DisplayAlertAsync
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }
#if ANDROID
                //This Xamarin.Forms Android issue no longer seems to be a problem in MAUI
                //Items = null
                //await System.Threading.Tasks.Task.Delay(400)
#endif
#if WINDOWS
                //MAUI bug https://github.com/dotnet/maui/issues/9739
                Items = [.. ((GetListResponse)response).List];
#endif
                Items = [.. ((GetListResponse)response).List];
                OnPropertyChanged(nameof(SelectedItem));

                this.Title = this._dropDownTemplate.TitleText;
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
                    entityType.FullName ?? throw new ArgumentException($"{nameof(entityType.FullName)}: {{65A904DC-79FE-4870-B6DA-EF68600CC7C4}}"), 
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

            Value = GetExistingValue() ?? this.defaultValue ?? default;

            IsValid = Validate();

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
                    ? this.defaultValue ?? default
                    : genericsHelpers.GetPropertyValue<T>(existing, _dropDownTemplate.ValueField);
            }
        }

        public override void Clear()
        {
            Items = null;
            Value = this.defaultValue ?? default;
            IsValid = Validate();
        }

        public ICommand SelectedIndexChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
