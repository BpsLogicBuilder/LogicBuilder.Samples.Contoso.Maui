using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogicBuilder.App.Maui.Services
{
    public interface IEntityStateUpdater
    {
        TModel GetUpdatedModel<TModel>(TModel? existingEntity, Dictionary<string, object?> existingAsDictionary, ObservableCollection<IValidatable> modifiedProperties, List<IFormItemSettingsDescriptor> fieldSettings);
    }
}
