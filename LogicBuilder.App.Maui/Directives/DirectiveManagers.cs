using Contoso.XPlatform.Directives.Factories;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Directives
{
    internal partial class DirectiveManagers<TModel> : IDirectiveManagers
    {
        public DirectiveManagers(IDirectiveManagersFactory directiveManagersFactory, ObservableCollection<IValidatable> properties, IFormGroupSettings formSettings)
        {
            this.properties = properties;
            this.formSettings = formSettings;

            validateIfManager = directiveManagersFactory.GetValidateIfManager
            (
                this.properties,
                GetConditions<ValidateIf<TModel>>()
            );

            hideIfManager = directiveManagersFactory.GetHideIfManager
            (
                this.properties,
                GetConditions<HideIf<TModel>>()
            );

            clearIfManager = directiveManagersFactory.GetClearIfManager
            (
                this.properties,
                GetConditions<ClearIf<TModel>>()
            );

            reloadIfManager = directiveManagersFactory.GetReloadIfManager
            (
                this.properties,
                GetConditions<ReloadIf<TModel>>()
            );

            List<TConditionBase> GetConditions<TConditionBase>() where TConditionBase : ConditionBase<TModel>, new()
                => directiveManagersFactory.GetDirectiveConditionsBuilder<TConditionBase, TModel>
                (
                    this.formSettings,
                    this.properties
                ).GetConditions();
        }

        private readonly ObservableCollection<IValidatable> properties;
        private readonly IFormGroupSettings formSettings;

        private readonly IValidateIfManager validateIfManager;
        private readonly IHideIfManager hideIfManager;
        private readonly IClearIfManager clearIfManager;
        private readonly IReloadIfManager reloadIfManager;

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DisposeComponent(validateIfManager);
                DisposeComponent(hideIfManager);
                DisposeComponent(clearIfManager);
                DisposeComponent(reloadIfManager);
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void DisposeComponent(IDisposable disposable)
        {
            disposable?.Dispose();
        }
    }
}
