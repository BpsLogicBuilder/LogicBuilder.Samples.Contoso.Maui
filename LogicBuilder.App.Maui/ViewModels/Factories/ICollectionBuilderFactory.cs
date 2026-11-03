using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.ViewModels.Factories
{
    public interface ICollectionBuilderFactory
    {
        ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(Type modelType, List<ItemBindingDescriptor> bindingDescriptors);
        IFieldsCollectionBuilder GetFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
        IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, DetailFormLayout? formLayout, string? parentName);
        IUpdateOnlyFieldsCollectionBuilder GetUpdateOnlyFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName);
    }
}
