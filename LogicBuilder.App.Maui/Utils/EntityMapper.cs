using AutoMapper;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    public static class EntityMapper
    {
        public static Dictionary<string, object?> EntityToObjectDictionary(this object? entity, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings)
            => mapper.Map<Dictionary<string, object?>>(entity).ToObjectDictionaryFromEntity(mapper, fieldSettings);

        //public static Dictionary<string, object?> ValidatableListToObjectDictionary(this IEnumerable<IValidatable> properties, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings)
        //    => properties.ToDictionary(p => p.Name, p => p.Value).ToObjectDictionaryFromValidatableObjects(mapper, fieldSettings);

        public static Dictionary<string, object?> ValidatableListToObjectDictionary(this IEnumerable<IValidatable> properties, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings)
            => new ConvertToObjectDictionaryFromValidatableObjects(properties.ToDictionary(p => p.Name, p => p.Value), mapper, fieldSettings).Convert();

        public static object ToModelObject(this IEnumerable<IValidatable> properties, Type entityType, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings, object? destination = null)
        {
            MethodInfo methodInfo = (
                typeof(EntityMapper).GetMethod
                (
                    nameof(ToModelObject),
                    1,
                    [
                        typeof(IEnumerable<IValidatable>),
                        typeof(IMapper),
                        typeof(List<IFormItemSettingsDescriptor>),
                        entityType
                    ]
                ) ?? throw new ArgumentException($"{nameof(ToModelObject)}: {{9054A00C-E98D-4F57-84AF-06EC13899654}}")
            ).MakeGenericMethod(entityType); /*ToModelObject is local*/

            return methodInfo.Invoke(null, [properties, mapper, fieldSettings, destination])!;
        }

        public static T ToModelObject<T>(this IEnumerable<IValidatable> properties, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings, T? destination = null) where T : class
        {
            if (destination == null)
            {
                return mapper.Map<T>
                (
                    properties.ValidatableListToObjectDictionary(mapper, fieldSettings)
                );
            }

            return mapper.Map(properties.ValidatableListToObjectDictionary(mapper, fieldSettings), destination);
        }

        ///// <summary>
        ///// Ensures all child objects and collections are dictionaries
        ///// </summary>
        ///// <param name="propertiesDictionary"></param>
        ///// <param name="mapper"></param>
        ///// <param name="fieldSettings"></param>
        ///// <param name="parentField"></param>
        ///// <returns></returns>
        //private static Dictionary<string, object?> ToObjectDictionaryFromValidatableObjects(this IDictionary<string, object?> propertiesDictionary, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings, string? parentField = null)
        //{
        //    return fieldSettings.Aggregate(new Dictionary<string, object?>(), DoAggregation);

        //    Dictionary<string, object?> DoAggregation(Dictionary<string, object?> objectDictionary, IFormItemSettingsDescriptor setting)
        //    {
        //        if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
        //            AddMultiSelects(multiSelectFormControlSetting, objectDictionary);
        //        else if (setting is InputFieldControlSettingsDescriptor inputFieldControlSetting)
        //            AddInputFieldValueField(inputFieldControlSetting, objectDictionary);
        //        else if (setting is DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
        //            AddDropDownValueField(dropdownSelectorControlSetting, objectDictionary);
        //        else if (setting is FormGroupSettingsDescriptor formGroupSetting)
        //        {
        //            if (formGroupSetting.FormGroupTemplate == null)
        //                throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: E29413BD-6DC7-47D1-9986-B613E0568AFE");

        //            if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
        //                AddFormGroupInline(formGroupSetting, objectDictionary);
        //            else if (formGroupSetting.FormGroupTemplate?.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
        //                AddFormGroupPopup(formGroupSetting, objectDictionary);
        //            else
        //                throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CE249F5F-5645-4B74-BA17-5E1A9A5E73C8");
        //        }
        //        else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
        //            AddFormGroupArray(formGroupArraySetting, objectDictionary);
        //        else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
        //        {
        //            objectDictionary = groupBoxSettingsDescriptor.FieldSettings.Aggregate(objectDictionary, DoAggregation);
        //        }
        //        else
        //            throw new ArgumentException($"{nameof(setting)}: 82FDF6AC-C2DB-4655-AA03-673E5C6B4E0A");

        //        return objectDictionary;
        //    }

        //    void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        //    (
        //        formGroupArraySetting.Field,
        //        mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object?>>>
        //        (
        //            (IEnumerable<object>)(propertiesDictionary[GetFieldName(formGroupArraySetting.Field)] ?? new List<object>())
        //        )
        //        .Select
        //        (
        //            dictionary => dictionary.ToObjectDictionaryFromValidatableObjects(mapper, formGroupArraySetting.FieldSettings)
        //        ).ToList()//Need an ICollection<Dictionary<string, object?>>
        //    );

        //    void AddFormGroupPopup(FormGroupSettingsDescriptor formGroupSetting, Dictionary<string, object?> objectDictionary)
        //    {
        //        if (propertiesDictionary[GetFieldName(formGroupSetting.Field)] == null)
        //        {
        //            objectDictionary.Add(formGroupSetting.Field, null);
        //            return;
        //        }

        //        objectDictionary.Add
        //        (
        //            formGroupSetting.Field,
        //            mapper.Map<Dictionary<string, object?>>
        //            (
        //                propertiesDictionary[GetFieldName(formGroupSetting.Field)]
        //            ).ToObjectDictionaryFromValidatableObjects(mapper, formGroupSetting.FieldSettings)
        //        );
        //    }

        //    void AddFormGroupInline(FormGroupSettingsDescriptor formGroupSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        //    (
        //        formGroupSetting.Field,
        //        ToObjectDictionaryFromValidatableObjects
        //        (
        //            propertiesDictionary,
        //            mapper,
        //            formGroupSetting.FieldSettings,
        //            GetFieldName(formGroupSetting.Field)
        //        )
        //    );

        //    void AddMultiSelects(MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting, Dictionary<string, object?> objectDictionary)
        //    {
        //        if (propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)] == null)
        //        {//The mapper.Map<IEnumerable, IEnumerable>(null) behavior in the Xamarin runtime is different
        //         //form the local tests.  Use the null check to ensure the same behavior.
        //            objectDictionary.Add(multiSelectFormControlSetting.Field, null);
        //            return;
        //        }

        //        objectDictionary.Add
        //        (
        //            multiSelectFormControlSetting.Field,
        //            mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object?>>>
        //            (
        //                (IEnumerable<object>)(propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)] ?? new List<object>())
        //            )
        //        );
        //    }

        //    void AddInputFieldValueField(InputFieldControlSettingsDescriptor controlSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        //    (
        //        controlSetting.Field,
        //        propertiesDictionary[GetFieldName(controlSetting.Field)]
        //    );

        //    void AddDropDownValueField(DropdownSelectorControlSettingsDescriptor controlSetting, Dictionary<string, object?> objectDictionary) => objectDictionary.Add
        //    (
        //        controlSetting.Field,
        //        propertiesDictionary[GetFieldName(controlSetting.Field)]
        //    );

        //    string GetFieldName(string field)
        //        => string.IsNullOrEmpty(parentField)
        //            ? field
        //            : $"{parentField}.{field}";
        //}

        private static Dictionary<string, object?> ToObjectDictionaryFromEntity(this Dictionary<string, object?> propertiesDictionary, IMapper mapper, List<IFormItemSettingsDescriptor> fieldSettings)
        {
            if (propertiesDictionary.IsEmpty())//object must be null - no fields to update
                return [];

            return fieldSettings.Aggregate(new Dictionary<string, object?>(), DoAggregation);

            Dictionary<string, object?> DoAggregation(Dictionary<string, object?> objectDictionary, IFormItemSettingsDescriptor setting)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects(multiSelectFormControlSetting);
                else if (setting is InputFieldControlSettingsDescriptor inputFieldControlSetting)
                    AddInputFieldValueField(inputFieldControlSetting);
                else if (setting is DropdownSelectorControlSettingsDescriptor dropdownSelectorControlSetting)
                    AddDropdownValueField(dropdownSelectorControlSetting);
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    AddFormGroup(formGroupSetting);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting);
                else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
                {
                    objectDictionary = groupBoxSettingsDescriptor.FieldSettings.Aggregate(objectDictionary, DoAggregation);
                }
                else
                    throw new ArgumentException($"{nameof(setting)}: CC7AD9E6-1CA5-4B9D-B1DF-D28AF8D6D757");

                return objectDictionary;

                void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting) => objectDictionary.Add
                (
                    formGroupArraySetting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object?>>>
                    (
                        (IEnumerable<object>)(propertiesDictionary[formGroupArraySetting.Field] ?? new List<object>())
                    )
                    .Select
                    (
                        dictionary => dictionary.ToObjectDictionaryFromEntity(mapper, formGroupArraySetting.FieldSettings)
                    ).ToList()//Need an ICollection<Dictionary<string, object?>>
                );

                void AddFormGroup(FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (propertiesDictionary[formGroupSetting.Field] == null)
                    {
                        objectDictionary.Add(formGroupSetting.Field, null);
                        return;
                    }

                    objectDictionary.Add
                    (
                        formGroupSetting.Field,
                        mapper.Map<Dictionary<string, object?>>
                        (
                            propertiesDictionary[formGroupSetting.Field]
                        ).ToObjectDictionaryFromEntity(mapper, formGroupSetting.FieldSettings)
                    );
                }

                void AddMultiSelects(MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                {
                    if (propertiesDictionary[multiSelectFormControlSetting.Field] == null)
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
                            (IEnumerable<object>)(propertiesDictionary[multiSelectFormControlSetting.Field] ?? new List<object>())
                        )
                    );
                }

                void AddInputFieldValueField(InputFieldControlSettingsDescriptor controlSetting) => objectDictionary.Add
                (
                    controlSetting.Field,
                    propertiesDictionary[controlSetting.Field]
                );

                void AddDropdownValueField(DropdownSelectorControlSettingsDescriptor controlSetting) => objectDictionary.Add
                (
                    controlSetting.Field,
                    propertiesDictionary[controlSetting.Field]
                );
            }
        }

        const string EntityState = "EntityState";
        public static void UpdateEntityStates(Dictionary<string, object?> existing, Dictionary<string, object?> current, List<IFormItemSettingsDescriptor> fieldSettings)
        {
            if (current.IsEmpty() && !existing.IsEmpty())
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                return;
            }

            if (current.IsEmpty() && existing.IsEmpty())
            {
                return;
            }

            if (existing.IsEmpty() && !current.IsEmpty())
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
            }
            else
            {
                current[EntityState] = new DictionaryComparer().Equals(current, existing)
                    ? LogicBuilder.Domain.EntityStateType.Unchanged
                    : LogicBuilder.Domain.EntityStateType.Modified;
            }

            foreach (var setting in fieldSettings)
            {
                UpdateFieldSettingEntityState(existing, current, setting);
            }
        }

        private static void UpdateFieldSettingEntityState(Dictionary<string, object?> existing, Dictionary<string, object?> current, IFormItemSettingsDescriptor setting)
        {
            if (setting is FormGroupSettingsDescriptor formGroupSetting)
            {
                existing.TryGetValue(formGroupSetting.Field, out object? existingObject);
                UpdateEntityStates
                (
                    (Dictionary<string, object?>)(existingObject ?? new Dictionary<string, object?>()),
                    (Dictionary<string, object?>)(current[formGroupSetting.Field] ?? new Dictionary<string, object?>()),
                    formGroupSetting.FieldSettings
                );
            }
            else if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettingsDescriptor)
            {
                UpdateMultiSelectEntityStates(existing, current, multiSelectFormControlSettingsDescriptor);
            }
            else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
            {
                UpdateFormGroupArrayEntityStates(existing, current, formGroupArraySetting);
            }
            else if (setting is FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor)
            {
                UpdateEntityStates(existing, current, groupBoxSettingsDescriptor.FieldSettings);
            }
        }

        private static void UpdateMultiSelectEntityStates(Dictionary<string, object?> existing, Dictionary<string, object?> current, MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettingsDescriptor)
        {
            existing.TryGetValue(multiSelectFormControlSettingsDescriptor.Field, out object? existingCollection);
            ICollection<Dictionary<string, object?>> existingList = (ICollection<Dictionary<string, object?>>)(existingCollection ?? new List<Dictionary<string, object?>>());
            ICollection<Dictionary<string, object?>> currentList = (ICollection<Dictionary<string, object?>>)(current[multiSelectFormControlSettingsDescriptor.Field] ?? new List<Dictionary<string, object?>>());

            if (currentList.Count != 0)
            {
                foreach (var entry in currentList)
                {
                    if (entry.ExistsInList(existingList, multiSelectFormControlSettingsDescriptor.KeyFields))
                        entry[EntityState] = LogicBuilder.Domain.EntityStateType.Unchanged;
                    else
                        entry[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
                }
            }

            if (existingList.Count != 0)
            {
                foreach (var entry in existingList.Where(e => !e.ExistsInList(currentList, multiSelectFormControlSettingsDescriptor.KeyFields)))
                {
                    entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                    currentList.Add(entry);
                }
            }
        }

        private static void UpdateFormGroupArrayEntityStates(Dictionary<string, object?> existing, Dictionary<string, object?> current, FormGroupArraySettingsDescriptor formGroupArraySetting)
        {
            existing.TryGetValue(formGroupArraySetting.Field, out object? existingCollection);
            ICollection<Dictionary<string, object?>> existingList = (ICollection<Dictionary<string, object?>>)(existingCollection ?? new List<Dictionary<string, object?>>());
            ICollection<Dictionary<string, object?>> currentList = (ICollection<Dictionary<string, object?>>)(current[formGroupArraySetting.Field] ?? new List<Dictionary<string, object?>>());

            if (currentList.Count != 0)
            {
                foreach (var entry in currentList)
                {
                    Dictionary<string, object?>? existingEntry = entry.GetExistingEntry(existingList, formGroupArraySetting.KeyFields);
                    UpdateEntityStates
                    (
                        existingEntry ?? [],
                        entry,
                        formGroupArraySetting.FieldSettings
                    );
                }
            }

            if (existingList.Count != 0)
            {
                foreach (var entry in existingList.Where(e => !e.ExistsInList(currentList, formGroupArraySetting.KeyFields)))
                {
                    entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                    currentList.Add(entry);
                }
            }
        }

        private static bool IsEmpty(this IDictionary<string, object?> dictionary)
            => dictionary?.Any() != true;
    }
}
