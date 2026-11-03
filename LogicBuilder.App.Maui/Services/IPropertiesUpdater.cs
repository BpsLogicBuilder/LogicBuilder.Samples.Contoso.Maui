using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Services
{
    public interface IPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IValidatable> properties, object? entity, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null);
    }
}
