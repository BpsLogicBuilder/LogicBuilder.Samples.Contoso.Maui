using AutoMapper;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.App.Maui.Services
{
    public class PropertiesUpdater(IMapper mapper) : IPropertiesUpdater
    {
        private readonly IMapper mapper = mapper;

        public void UpdateProperties(IEnumerable<IValidatable> properties, object? entity, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null) 
            => UpdateValidatables(properties, entity, fieldSettings, parentField);

        private void UpdateValidatables(IEnumerable<IValidatable> properties, object? source, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null)
        {
            Dictionary<string, object?> existingValues = mapper.Map<Dictionary<string, object?>>(source) ?? [];
            Dictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                {
                    UpdateMultiSelectValidatables(parentField, existingValues, propertiesDictionary, multiSelectFormControlSetting);
                }
                else if (setting is InputFieldControlSettingsDescriptor inputFieldControlSetting)
                {
                    UpdateInputFieldValidatables(parentField, existingValues, propertiesDictionary, inputFieldControlSetting);
                }
                else if (setting is DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
                {
                    UpdateDropdownSelectorValidatables(parentField, existingValues, propertiesDictionary, dropdownSelectorControlSetting);
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    UpdateFormGroupValidatables(properties, parentField, existingValues, propertiesDictionary, formGroupSetting);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    UpdateFormGroupArrayValidatables(parentField, existingValues, propertiesDictionary, formGroupArraySetting);
                }
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    UpdateValidatables(properties, source, groupBoxSettingsDescriptor.FieldSettings, parentField);
                }
            }
        }

        private static void UpdateMultiSelectValidatables(string? parentField, Dictionary<string, object?> existingValues, Dictionary<string, IValidatable> propertiesDictionary, MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
        {
            if (existingValues.TryGetValue(multiSelectFormControlSetting.Field, out object? @value) && @value != null)
            {
                IValidatable multiSelectValidatable = propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field, parentField)];

                if (!Utils.TypeHelpers.AssignableFrom(multiSelectValidatable.Type.GetUnderlyingElementType(), @value.GetType().GetUnderlyingElementType()))
                    throw new ArgumentException($"{nameof(multiSelectFormControlSetting)}: 74B8794A-9C00-4A25-8089-10957DF0A5EC");

                multiSelectValidatable.Value = Activator.CreateInstance
                (
                    multiSelectValidatable.Type,
                    [@value]
                ) ?? throw new ArgumentException($"{multiSelectValidatable.Type}: {{C3ABA8E6-A30F-485B-9D33-AF60D72E8AD0}}");
            }
        }

        private static void UpdateInputFieldValidatables(string? parentField, Dictionary<string, object?> existingValues, Dictionary<string, IValidatable> propertiesDictionary, InputFieldControlSettingsDescriptor inputFieldControlSetting)
        {
            if (existingValues.TryGetValue(inputFieldControlSetting.Field, out object? @value) && @value != null)
            {
                if (!Utils.TypeHelpers.AssignableFrom(propertiesDictionary[GetFieldName(inputFieldControlSetting.Field, parentField)].Type, @value.GetType()))
                    throw new ArgumentException($"{nameof(inputFieldControlSetting)}: F4B014E4-B04C-455D-8AE5-1F2551BEC190");

                propertiesDictionary[GetFieldName(inputFieldControlSetting.Field, parentField)].Value = @value;
            }
        }

        private static void UpdateDropdownSelectorValidatables(string? parentField, Dictionary<string, object?> existingValues, Dictionary<string, IValidatable> propertiesDictionary, DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
        {
            if (existingValues.TryGetValue(dropdownSelectorControlSetting.Field, out object? @value) && @value != null)
            {
                if (!Utils.TypeHelpers.AssignableFrom(propertiesDictionary[GetFieldName(dropdownSelectorControlSetting.Field, parentField)].Type, @value.GetType()))
                    throw new ArgumentException($"{nameof(dropdownSelectorControlSetting)}: 02FB0E38-D311-43B5-A350-9C5F5F4B1A13");

                propertiesDictionary[GetFieldName(dropdownSelectorControlSetting.Field, parentField)].Value = @value;
            }
        }

        private void UpdateFormGroupValidatables(IEnumerable<IValidatable> properties, string? parentField, Dictionary<string, object?> existingValues, Dictionary<string, IValidatable> propertiesDictionary, FormGroupSettingsDescriptor formGroupSetting)
        {
            if (existingValues.TryGetValue(formGroupSetting.Field, out object? @value) && @value != null)
            {
                if (formGroupSetting.FormGroupTemplate == null)
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 74E0697E-B5EF-4939-B0B4-8B7E4AE5544B");

                if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                    UpdateValidatables(properties, @value, formGroupSetting.FieldSettings, GetFieldName(formGroupSetting.Field, parentField));
                else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                {
                    if (!Utils.TypeHelpers.AssignableFrom(propertiesDictionary[GetFieldName(formGroupSetting.Field, parentField)].Type, @value.GetType()))
                        throw new ArgumentException($"{nameof(formGroupSetting)}: 83ADA1B9-5951-4643-BE40-E9BB6DB45C06");

                    propertiesDictionary[GetFieldName(formGroupSetting.Field, parentField)].Value = @value;
                }
                else
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 5504FE49-2766-4D7C-916D-8FC633477DB1");
            }
        }

        private static void UpdateFormGroupArrayValidatables(string? parentField, Dictionary<string, object?> existingValues, Dictionary<string, IValidatable> propertiesDictionary, FormGroupArraySettingsDescriptor formGroupArraySetting)
        {
            if (existingValues.TryGetValue(formGroupArraySetting.Field, out object? @value) && @value != null)
            {
                IValidatable forArrayValidatable = propertiesDictionary[GetFieldName(formGroupArraySetting.Field, parentField)];

                if (!Utils.TypeHelpers.AssignableFrom(forArrayValidatable.Type.GetUnderlyingElementType(), @value.GetType().GetUnderlyingElementType()))
                    throw new ArgumentException($"{nameof(formGroupArraySetting)}: CCB454D1-8119-475B-9A2B-1EB10E513959");

                forArrayValidatable.Value = Activator.CreateInstance
                (
                    forArrayValidatable.Type,
                    [@value]
                ) ?? throw new ArgumentException($"{forArrayValidatable.Type}: {{C6549FB5-239A-429F-9B73-80959F159EF8}}");
            }
        }

        static string GetFieldName(string field, string? parentField)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
    }
}
