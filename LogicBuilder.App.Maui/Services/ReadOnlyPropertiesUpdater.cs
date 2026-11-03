using AutoMapper;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogicBuilder.App.Maui.Services
{
    public class ReadOnlyPropertiesUpdater(IMapper mapper) : IReadOnlyPropertiesUpdater
    {
        private readonly IMapper mapper = mapper;

        public void UpdateProperties(IEnumerable<IReadOnly> properties, object? entity, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null) 
            => UpdateReadOnlys(properties, entity, fieldSettings, parentField);

        private void UpdateReadOnlys(IEnumerable<IReadOnly> properties, object? source, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null)
        {
            Dictionary<string, object?> existingValues = mapper.Map<Dictionary<string, object?>>(source) ?? [];
            Dictionary<string, IReadOnly> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectDetailControlSetting)
                {
                    UpdateMultiSelectReadOnlys(existingValues, propertiesDictionary, multiSelectDetailControlSetting, parentField);
                }
                else if (setting is InputFieldControlSettingsDescriptor inputFieldControlSetting)
                {
                    UpdateInputFieldReadOnlys(existingValues, propertiesDictionary, inputFieldControlSetting, parentField);
                }
                else if (setting is DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
                {
                    UpdateDropdownSelectorReadOnlys(existingValues, propertiesDictionary, dropdownSelectorControlSetting, parentField);
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    UpdateRFormGroupeadOnlys(properties, existingValues, propertiesDictionary, formGroupSetting, parentField);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    UpdateFormGroupArrayReadOnlys(existingValues, propertiesDictionary, formGroupArraySetting, parentField);
                }
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    UpdateReadOnlys(properties, source, groupBoxSettingsDescriptor.FieldSettings, parentField);
                }
            }
        }

        static string GetFieldName(string field, string? parentField)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";

        static void UpdateFormGroupArrayReadOnlys(Dictionary<string, object?> existingValues, Dictionary<string, IReadOnly> propertiesDictionary, FormGroupArraySettingsDescriptor formGroupArraySetting, string? parentField)
        {
            if (existingValues.TryGetValue(formGroupArraySetting.Field, out object? @value) && @value != null)
            {
                propertiesDictionary[GetFieldName(formGroupArraySetting.Field, parentField)].Value = Activator.CreateInstance
                (
                    typeof(ObservableCollection<>).MakeGenericType
                    (
                        Type.GetType(formGroupArraySetting.ModelType) ?? throw new ArgumentException($"{formGroupArraySetting.ModelType}: {{5901AF9C-4530-448D-B281-BC655263017A}}")
                    ),
                    [@value]
                ) ?? throw new ArgumentException($"{formGroupArraySetting.ModelType}: {{AD348E36-C66C-4E5F-B4A3-A3AE9A236F34}}");
            }
        }

        void UpdateRFormGroupeadOnlys(IEnumerable<IReadOnly> properties, Dictionary<string, object?> existingValues, Dictionary<string, IReadOnly> propertiesDictionary, FormGroupSettingsDescriptor formGroupSetting, string? parentField)
        {
            if (existingValues.TryGetValue(formGroupSetting.Field, out object? @value) && @value != null)
            {
                if (formGroupSetting.FormGroupTemplate == null)
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 85E77F0C-4913-48FE-B6AA-A9AC5D01BEFC");

                if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                    UpdateReadOnlys(properties, @value, formGroupSetting.FieldSettings, GetFieldName(formGroupSetting.Field, parentField));
                else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                    propertiesDictionary[GetFieldName(formGroupSetting.Field, parentField)].Value = @value;
                else
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 6CCB1B68-6D5B-4417-9074-70A536872EFA");
            }
        }

        static void UpdateDropdownSelectorReadOnlys(Dictionary<string, object?> existingValues, Dictionary<string, IReadOnly> propertiesDictionary, DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting, string? parentField)
        {
            if (existingValues.TryGetValue(dropdownSelectorControlSetting.Field, out object? @value) && @value != null)
                propertiesDictionary[GetFieldName(dropdownSelectorControlSetting.Field, parentField)].Value = @value;
        }

        static void UpdateInputFieldReadOnlys(Dictionary<string, object?> existingValues, Dictionary<string, IReadOnly> propertiesDictionary, InputFieldControlSettingsDescriptor inputFieldControlSetting, string? parentField)
        {
            if (existingValues.TryGetValue(inputFieldControlSetting.Field, out object? @value) && @value != null)
                propertiesDictionary[GetFieldName(inputFieldControlSetting.Field, parentField)].Value = @value;
        }

        static void UpdateMultiSelectReadOnlys(Dictionary<string, object?> existingValues, Dictionary<string, IReadOnly> propertiesDictionary, MultiSelectFormControlSettingsDescriptor multiSelectDetailControlSetting, string? parentField)
        {
            if (existingValues.TryGetValue(multiSelectDetailControlSetting.Field, out object? @value) && @value != null)
            {
                propertiesDictionary[GetFieldName(multiSelectDetailControlSetting.Field, parentField)].Value = Activator.CreateInstance
                (
                    typeof(ObservableCollection<>).MakeGenericType
                    (
                        Type.GetType(multiSelectDetailControlSetting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{multiSelectDetailControlSetting.MultiSelectTemplate.ModelType}: {{4AB21DE8-D32A-46F4-90C2-0E4E25F9E303}}")
                    ),
                    [@value]
                ) ?? throw new ArgumentException($"{multiSelectDetailControlSetting.MultiSelectTemplate.ModelType}: {{BF676A94-25FE-429D-862B-4FE2633FFBC9}}");
            }
        }
    }
}
