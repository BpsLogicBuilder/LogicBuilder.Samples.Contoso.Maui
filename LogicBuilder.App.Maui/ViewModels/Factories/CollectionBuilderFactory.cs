using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Forms.Configuration.Bindings;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.ViewModels.Factories
{
    internal class CollectionBuilderFactory(
        Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder> getCollectionCellItemsBuilder,
        Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> getFieldsCollectionBuilder,
        Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder> getReadOnlyFieldsCollectionBuilder,
        Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> getUpdateOnlyFieldsCollectionBuilder) : ICollectionBuilderFactory
    {
        public Func<Type, List<ItemBindingDescriptor>, ICollectionCellItemsBuilder> _getCollectionCellItemsBuilder = getCollectionCellItemsBuilder;
        public Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IFieldsCollectionBuilder> _getFieldsCollectionBuilder = getFieldsCollectionBuilder;
        public Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, DetailFormLayout?, string?, IReadOnlyFieldsCollectionBuilder> _getReadOnlyFieldsCollectionBuilder = getReadOnlyFieldsCollectionBuilder;
        public Func<Type, List<IFormItemSettingsDescriptor>, IFormGroupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>>, EditFormLayout?, string?, IUpdateOnlyFieldsCollectionBuilder> _getUpdateOnlyFieldsCollectionBuilder = getUpdateOnlyFieldsCollectionBuilder;

        public ICollectionCellItemsBuilder GetCollectionCellItemsBuilder(Type modelType, List<ItemBindingDescriptor> bindingDescriptors) 
            => _getCollectionCellItemsBuilder
            (
                modelType,
                bindingDescriptors
            );

        public IFieldsCollectionBuilder GetFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName) 
            => _getFieldsCollectionBuilder
            (
                modelType,
                fieldSettings,
                groupBoxSettings,
                validationMessages,
                formLayout,
                parentName
            );

        public IReadOnlyFieldsCollectionBuilder GetReadOnlyFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, DetailFormLayout? formLayout, string? parentName) 
            => _getReadOnlyFieldsCollectionBuilder
            (
                modelType,
                fieldSettings,
                groupBoxSettings,
                formLayout,
                parentName
            );

        public IUpdateOnlyFieldsCollectionBuilder GetUpdateOnlyFieldsCollectionBuilder(Type modelType, List<IFormItemSettingsDescriptor> fieldSettings, IFormGroupBoxSettings groupBoxSettings, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, EditFormLayout? formLayout, string? parentName)
        {
            return _getUpdateOnlyFieldsCollectionBuilder
            ( 
                modelType,
                fieldSettings,
                groupBoxSettings,
                validationMessages,
                formLayout,
                parentName
            );
        }
    }
}
