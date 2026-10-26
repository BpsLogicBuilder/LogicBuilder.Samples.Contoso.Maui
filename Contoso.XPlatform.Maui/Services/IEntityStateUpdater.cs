using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Services
{
    public interface IEntityStateUpdater
    {
        TModel GetUpdatedModel<TModel>(TModel? existingEntity, Dictionary<string, object?> existingAsDictionary, ObservableCollection<IValidatable> modifiedProperties, List<IFormItemSettingsDescriptor> fieldSettings);
    }
}
