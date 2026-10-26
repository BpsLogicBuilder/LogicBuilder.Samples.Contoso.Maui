using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System.Collections.Generic;

namespace Contoso.XPlatform.Services
{
    public interface IReadOnlyPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, object? entity, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null);
    }
}
