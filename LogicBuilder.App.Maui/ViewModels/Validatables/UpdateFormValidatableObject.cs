using Contoso.XPlatform.Directives;
using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Factories;
using Contoso.XPlatform.Views.Factories;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public partial class UpdateFormValidatableObject<T> : BaseFormValidatableObject<T> where T : class
    {
        public UpdateFormValidatableObject(
            ICollectionBuilderFactory collectionBuilderFactory,
            IDirectiveManagersFactory directiveManagersFactory,
            IEntityUpdater entityUpdater,
            IPopupFormFactory popupFormFactory,
            IPropertiesUpdater propertiesUpdater,
            IUiNotificationService uiNotificationService,
            string name,
            IChildFormGroupSettings setting,
            IEnumerable<IValidationRule>? validations) : base(name, setting.FormGroupTemplate.TemplateName, validations, uiNotificationService)
        {
            this.FormSettings = setting;
            this.Title = this.FormSettings.Title;
            this.Placeholder = this.FormSettings.ValidFormControlText;
            this.EntityUpdater = entityUpdater;
            this.PopupFormFactory = popupFormFactory;
            this.PropertiesUpdater = propertiesUpdater;
            this.FieldsCollectionBuilder = collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                typeof(T),
                this.FormSettings.FieldSettings,
                this.FormSettings,
                this.FormSettings.ValidationMessages,
                null,
                null
            );

            FormLayout = FieldsCollectionBuilder.CreateFields();

            this.DirectiveManagers = (DirectiveManagers<T>)directiveManagersFactory.GetDirectiveManagers
            (
                typeof(T),
                FormLayout.Properties,
                FormSettings
            );

            PropertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(FieldChanged);
        }

        public override EditFormLayout FormLayout { get; set; }

        public override IChildFormGroupSettings FormSettings { get; set; }
        protected override IEntityUpdater EntityUpdater { get; }
        protected override IPropertiesUpdater PropertiesUpdater { get; }
        protected override IDisposable PropertyChangedSubscription { get; }
        internal override DirectiveManagers<T> DirectiveManagers { get; }

        protected override IFieldsCollectionBuilder FieldsCollectionBuilder { get; }
        protected override IPopupFormFactory PopupFormFactory { get; }
    }
}
