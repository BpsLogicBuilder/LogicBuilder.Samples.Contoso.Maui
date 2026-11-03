using AutoMapper;
using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Factories;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.EditForm
{
    public partial class EditFormViewModel<TModel> : EditFormViewModelBase where TModel : LogicBuilder.Domain.BaseModel
    {
        public EditFormViewModel(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IEntityStateUpdater entityStateUpdater,
            IHttpService httpService,
            IMapper mapper,
            IPropertiesUpdater propertiesUpdater,
            IUiNotificationService uiNotificationService,
            IServiceProvider serviceProvider,
            ScreenSettings<DataFormSettingsDescriptor> screenSettings)
            : base(screenSettings, uiNotificationService)
        {
            FormLayout = collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                typeof(TModel),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                this.FormSettings.ValidationMessages,
                null,
                null
            ).CreateFields();

            this.entityStateUpdater = entityStateUpdater;
            this.httpService = httpService;
            this.propertiesUpdater = propertiesUpdater;
            this.mapper = mapper;
            this.ServiceProvider = serviceProvider;
            this.directiveManagers = (DirectiveManagers<TModel>)directiveManagersFactory.GetDirectiveManagers
            (
                typeof(TModel), 
                FormLayout.Properties, 
                FormSettings
            );

            propertyChangedSubscription = this.UiNotificationService.ValueChanged.Subscribe(FieldChanged);

            if (this.FormSettings.FormType == FormType.Update)
            {
                _ = GetEntity();
            }
        }

        public override EditFormLayout FormLayout { get; set; }

        private readonly IEntityStateUpdater entityStateUpdater;
        private readonly IHttpService httpService;
        private readonly IPropertiesUpdater propertiesUpdater;
        private readonly IMapper mapper;
        private readonly DirectiveManagers<TModel> directiveManagers;
        private TModel? entity;
        private Dictionary<string, object?> originalEntityDictionary = [];
        private readonly IDisposable propertyChangedSubscription;

        private bool disposed = false;

        protected override IServiceProvider ServiceProvider { get; }

        protected override void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                DisposeComponents(this.directiveManagers);
                DisposeComponents(this.propertyChangedSubscription);
                foreach (var property in FormLayout.Properties)
                {
                    if (property is IDisposable disposable)
                        DisposeComponents(disposable);
                }
            }

            base.Dispose(disposing);

            disposed = true;
        }

        protected static void DisposeComponents(IDisposable disposable)
        {
            disposable?.Dispose();
        }

        private void FieldChanged(string fieldName)
        {
            ((Command)SubmitCommand).ChangeCanExecute();
        }

        private async Task GetEntity()
        {
            if (this.FormSettings.RequestDetails?.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 883E834F-98A6-4DF7-9D07-F1BB0D6639E1");

            BaseResponse baseResponse = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
            (
                () => this.httpService.GetEntity
                (
                    new GetEntityRequest
                    {
                        Filter = this.FormSettings.RequestDetails.Filter,
                        SelectExpandDefinition = this.FormSettings.RequestDetails.SelectExpandDefinition,
                        ModelType = this.FormSettings.RequestDetails.ModelType,
                        DataType = this.FormSettings.RequestDetails.DataType
                    }
                )
            );

            if (!baseResponse.Success)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlertAsync/*App.Current.MainPage is not null here*/
                (
                    "Errors",
                    string.Join(Environment.NewLine, baseResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            GetEntityResponse getEntityResponse = (GetEntityResponse)baseResponse;
            this.entity = (TModel)(getEntityResponse.Entity ?? throw new InvalidOperationException("Entity cannot be null"));

            this.originalEntityDictionary = this.entity.EntityToObjectDictionary
            (
               mapper,
               this.FormSettings.FieldSettings
            );

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private ICommand? _submitCommand;
        public ICommand SubmitCommand => _submitCommand ??= new Command<CommandButtonDescriptor>
        (
            execute: async (button) =>
            {
                TModel toSave = this.entityStateUpdater.GetUpdatedModel
                (
                    entity,
                    this.originalEntityDictionary,
                    FormLayout.Properties,
                    FormSettings.FieldSettings
                )!;/*GetUpdatedModel does not return null*/

                if (toSave.EntityState == LogicBuilder.Domain.EntityStateType.Unchanged)
                {
                    Next(button);
                    return;
                }

                BaseResponse response = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
                (
                    () => this.httpService.SaveEntity
                    (
                        new SaveEntityRequest
                        {
                            Entity = toSave
                        },
                        this.FormSettings.FormType == FormType.Add
                            ? this.FormSettings.RequestDetails?.AddUrl ?? throw new InvalidOperationException($"{nameof(FormRequestDetailsDescriptor.AddUrl)} cannot be null")
                            : this.FormSettings.RequestDetails?.UpdateUrl ?? throw new InvalidOperationException($"{nameof(FormRequestDetailsDescriptor.UpdateUrl)} cannot be null")
                    )
                );

                if (!response.Success)
                {
                    await Application.Current!.Windows[0].Page!.DisplayAlertAsync/*App.Current.MainPage is not null here*/
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
                }

                Next(button);
            },
            canExecute: (button) => AreFieldsValid()
        );
    }
}
