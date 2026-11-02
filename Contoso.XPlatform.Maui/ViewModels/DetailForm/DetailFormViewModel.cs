using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Utils;
using Contoso.XPlatform.ViewModels.Factories;
using LogicBuilder.App.Bsl.Business.Requests;
using LogicBuilder.App.Bsl.Business.Responses;
using LogicBuilder.App.Maui.Business.Requests;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.DetailForm
{
    public partial class DetailFormViewModel<TModel> : DetailFormViewModelBase, IDisposable where TModel : Domain.Entities.EntityModelBase
    {
        public DetailFormViewModel(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IHttpService httpService,
            IReadOnlyPropertiesUpdater readOnlyPropertiesUpdater,
            IUiNotificationService uiNotificationService,
            ScreenSettings<DataFormSettingsDescriptor> screenSettings) 
            : base(screenSettings, uiNotificationService)
        {
            FormLayout = collectionBuilderFactory.GetReadOnlyFieldsCollectionBuilder
            (
                typeof(TModel),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                null,
                null
            ).CreateFields();

            this.httpService = httpService;
            this.propertiesUpdater = readOnlyPropertiesUpdater;

            this.directiveManagers = (ReadOnlyDirectiveManagers<TModel>)directiveManagersFactory.GetReadOnlyDirectiveManagers
            (
                typeof(TModel), 
                FormLayout.Properties, 
                FormSettings
            );
            
            _ = GetEntity();
        }

        private readonly IHttpService httpService;
        private readonly IReadOnlyPropertiesUpdater propertiesUpdater;
        private readonly ReadOnlyDirectiveManagers<TModel> directiveManagers;
        private TModel? entity;

        private ICommand? _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand != null)
                    return _deleteCommand;

                _deleteCommand = new Command<CommandButtonDescriptor>
                (
                     async (button) => 
                     {
                         BaseResponse response = await BusyIndicatorHelpers.ExecuteRequestWithBusyIndicator
                        (
                            () => this.httpService.DeleteEntity
                            (
                                new DeleteEntityRequest
                                {
                                    Entity = entity
                                },
                                this.FormSettings.RequestDetails?.DeleteUrl ?? throw new InvalidOperationException($"{nameof(FormRequestDetailsDescriptor.DeleteUrl)} cannot be null")
                            )
                        );

                         if (!response.Success)
                         {
                             await App.Current!.Windows[0].Page!.DisplayAlertAsync/*App.Current.MainPage is not null at this point*/
                             (
                                 "Errors",
                                 string.Join(Environment.NewLine, response.ErrorMessages),
                                 "Ok"
                             );
                         }

                         Next(button);
                     }
                );

                return _deleteCommand;
            }
        }

        private ICommand? _editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_editCommand != null)
                    return _editCommand;

                _editCommand = new Command<CommandButtonDescriptor>
                (
                    Edit,
                    (button) => this.entity != null
                );

                return _editCommand;
            }
        }

        public override DetailFormLayout FormLayout { get; set; }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DisposeComponent(this.directiveManagers);
                foreach (var property in FormLayout.Properties)
                {
                    if (property is IDisposable disposable)
                        DisposeComponent(disposable);
                }
            }
            disposed = true;
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

        private void Edit(CommandButtonDescriptor button)
        {
            _ = SetEntityAndNavigateNext(button);
        }

        private async Task GetEntity()
        {
            if (this.FormSettings.RequestDetails?.Filter == null)
                throw new ArgumentException($"{nameof(this.FormSettings.RequestDetails.Filter)}: 51755FE3-099A-44EB-A59B-3ED312EDD8D1");

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
                await App.Current!.Windows[0].Page!.DisplayAlertAsync/*App.Current.MainPage is not null at this point*/
                (
                    "Errors",
                    string.Join(Environment.NewLine, baseResponse.ErrorMessages),
                    "Ok"
                );
                return;
            }

            GetEntityResponse getEntityResponse = (GetEntityResponse)baseResponse;
            this.entity = (TModel)(getEntityResponse.Entity ?? throw new InvalidOperationException("Entity cannot be null"));
            ((Command)EditCommand).ChangeCanExecute();

            this.propertiesUpdater.UpdateProperties
            (
                FormLayout.Properties,
                getEntityResponse.Entity,
                this.FormSettings.FieldSettings
            );
        }

        private async Task SetEntityAndNavigateNext(CommandButtonDescriptor button)
        {
            if (entity == null)
                throw new ArgumentException($"{nameof(entity)}: {{97F661A5-1AE3-4A85-8083-438B665A58B7}}");

            using IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>();
            flowManagerService.CopyFlowItems();

            flowManagerService.SetFlowDataCacheItem
            (
                typeof(TModel).FullName!,
                this.entity
            );

            await flowManagerService.Next
            (
                new CommandButtonRequest
                {
                    NewSelection = button.ShortString
                }
            );
        }
    }
}
