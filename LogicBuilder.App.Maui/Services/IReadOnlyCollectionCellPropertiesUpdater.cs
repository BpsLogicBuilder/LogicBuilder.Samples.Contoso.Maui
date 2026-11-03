using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using System;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Services
{
    public interface IReadOnlyCollectionCellPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, Type modelType, object entity, List<ItemBindingDescriptor> itemBindings);
    }
}
