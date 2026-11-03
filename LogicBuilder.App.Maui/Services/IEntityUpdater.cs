using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogicBuilder.App.Maui.Services
{
    public interface IEntityUpdater
    {
        TModel ToModelObject<TModel>(ObservableCollection<IValidatable> modifiedProperties, List<IFormItemSettingsDescriptor> fieldSettings, TModel? destination = null) where TModel : class;
        object ToModelObject(Type modelType, ObservableCollection<IValidatable> modifiedProperties, List<IFormItemSettingsDescriptor> fieldSettings, object? destination = null);
    }
}
