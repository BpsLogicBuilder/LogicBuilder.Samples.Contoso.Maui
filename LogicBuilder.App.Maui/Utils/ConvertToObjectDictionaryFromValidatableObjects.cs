using AutoMapper;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.App.Maui.Utils
{
    internal class ConvertToObjectDictionaryFromValidatableObjects(IDictionary<string, object?> propertiesDictionary, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null)
    {
        private readonly IDictionary<string, object?> propertiesDictionary = propertiesDictionary;
        private readonly IMapper mapper = mapper;
        private readonly List<IFormItemSettingsDescriptor> fieldSettings = fieldSettings;
        private readonly string? parentField = parentField;

        public Dictionary<string, object?> Convert()
        {
            return fieldSettings.Aggregate(new Dictionary<string, object?>(), DoAggregation);

            Dictionary<string, object?> DoAggregation(Dictionary<string, object?> objectDictionary, IFormItemSettingsDescriptor setting)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects(multiSelectFormControlSetting, objectDictionary);
                else if (setting is InputFieldControlSettingsDescriptor inputFieldControlSetting)
                    AddInputFieldValueField(inputFieldControlSetting, objectDictionary);
                else if (setting is DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
                    AddDropDownValueField(dropdownSelectorControlSetting, objectDictionary);
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    AddFormGroup(objectDictionary, formGroupSetting);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting, objectDictionary);
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    objectDictionary = groupBoxSettingsDescriptor.FieldSettings.Aggregate(objectDictionary, DoAggregation);
                }
                else
                    throw new ArgumentException($"{nameof(setting)}: 82FDF6AC-C2DB-4655-AA03-673E5C6B4E0A");

                return objectDictionary;
            }
        }

        private void AddFormGroup(Dictionary<string, object?> objectDictionary, FormGroupSettingsDescriptor formGroupSetting)
        {
            if (formGroupSetting.FormGroupTemplate == null)
                throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: E29413BD-6DC7-47D1-9986-B613E0568AFE");

            if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                AddFormGroupInline(formGroupSetting, objectDictionary);
            else if (formGroupSetting.FormGroupTemplate?.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                AddFormGroupPopup(formGroupSetting, objectDictionary);
            else
                throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CE249F5F-5645-4B74-BA17-5E1A9A5E73C8");
        }

        void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        (
            formGroupArraySetting.Field,
            mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object?>>>
            (
                (IEnumerable<object>)(propertiesDictionary[GetFieldName(formGroupArraySetting.Field)] ?? new List<object>())
            )
            .Select
            (
                dictionary => new ConvertToObjectDictionaryFromValidatableObjects(dictionary, mapper, formGroupArraySetting.FieldSettings).Convert()
            ).ToList()//Need an ICollection<Dictionary<string, object?>>
        );

        void AddFormGroupPopup(FormGroupSettingsDescriptor formGroupSetting, Dictionary<string, object?> objectDictionary)
        {
            if (propertiesDictionary[GetFieldName(formGroupSetting.Field)] == null)
            {
                objectDictionary.Add(formGroupSetting.Field, null);
                return;
            }

            objectDictionary.Add
            (
                formGroupSetting.Field,
                new ConvertToObjectDictionaryFromValidatableObjects
                (
                    mapper.Map<Dictionary<string, object?>>
                    (
                        propertiesDictionary[GetFieldName(formGroupSetting.Field)]
                    ), 
                    mapper,
                    formGroupSetting.FieldSettings
                ).Convert()
            );
        }

        void AddFormGroupInline(FormGroupSettingsDescriptor formGroupSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        (
            formGroupSetting.Field,
            new ConvertToObjectDictionaryFromValidatableObjects
            (
                propertiesDictionary,
                mapper,
                formGroupSetting.FieldSettings,
                GetFieldName(formGroupSetting.Field)
            ).Convert()
        );

        void AddMultiSelects(MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting, Dictionary<string, object?> objectDictionary)
        {
            if (propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)] == null)
            {//The mapper.Map<IEnumerable, IEnumerable>(null) behavior in the Xamarin runtime is different
             //form the local tests.  Use the null check to ensure the same behavior.
                objectDictionary.Add(multiSelectFormControlSetting.Field, null);
                return;
            }

            objectDictionary.Add
            (
                multiSelectFormControlSetting.Field,
                mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object?>>>
                (
                    (IEnumerable<object>)(propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)] ?? new List<object>())
                )
            );
        }

        void AddInputFieldValueField(InputFieldControlSettingsDescriptor controlSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        (
            controlSetting.Field,
            propertiesDictionary[GetFieldName(controlSetting.Field)]
        );

        void AddDropDownValueField(DropdownSelectorControlSettingsDescriptor controlSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        (
            controlSetting.Field,
            propertiesDictionary[GetFieldName(controlSetting.Field)]
        );

        string GetFieldName(string field)
            => string.IsNullOrEmpty(parentField)
                ? field
                : $"{parentField}.{field}";
    }
}
