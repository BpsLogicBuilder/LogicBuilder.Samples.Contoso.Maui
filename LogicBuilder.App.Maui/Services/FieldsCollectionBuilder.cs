using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.Validators;
using LogicBuilder.App.Maui.Validators.Rules;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.ViewModels.Validatables.Factories;
using LogicBuilder.App.Maui.Forms.Configuration;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace LogicBuilder.App.Maui.Services
{
    public class FieldsCollectionBuilder : IFieldsCollectionBuilder
    {
        protected readonly ICollectionBuilderFactory collectionBuilderFactory;
        private readonly IValidatableFactory validatableFactory;
        private readonly IValidatableValueHelper validatableValueHelper;

        private readonly List<IFormItemSettingsDescriptor> fieldSettings;
        protected IFormGroupBoxSettings groupBoxSettings;
        protected EditFormLayout formLayout;
        
        protected readonly string? parentName;
        protected readonly Type modelType;

#pragma warning disable S107 //Needed for field generation
        public FieldsCollectionBuilder(
            ICollectionBuilderFactory collectionBuilderFactory,
            IValidatableFactory validatableFactory,
            IValidatableValueHelper validatableValueHelper,
            List<IFormItemSettingsDescriptor> fieldSettings,
            IFormGroupBoxSettings groupBoxSettings,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            Type modelType,
            EditFormLayout? formLayout = null,
            string? parentName = null)
#pragma warning restore S107 //Needed for field generation
        {
            this.fieldSettings = fieldSettings;
            this.groupBoxSettings = groupBoxSettings;
            this.ValidationMessages = validationMessages;
            this.collectionBuilderFactory = collectionBuilderFactory;
            this.validatableFactory = validatableFactory;
            this.validatableValueHelper = validatableValueHelper;
            this.modelType = modelType;

            if (formLayout == null)
            {
                this.formLayout = new EditFormLayout();
                if (this.fieldSettings.ShouldCreateDefaultControlGroupBox())
                    this.formLayout.AddControlGroupBox(this.groupBoxSettings);
            }
            else
            {
                this.formLayout = formLayout;
            }

            this.parentName = parentName;
        }

        protected Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }

        public EditFormLayout CreateFields()
        {
            this.CreateFieldsCollection(this.fieldSettings);
            return this.formLayout;
        }

        private void CreateFieldsCollection(List<IFormItemSettingsDescriptor> fieldSettings)
        {
            fieldSettings.ForEach
            (
                setting =>
                {
                    switch (setting)
                    {
                        case MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettings:
                            AddMultiSelectControl(multiSelectFormControlSettings);
                            break;
                        case InputFieldControlSettingsDescriptor inputFieldSettings:
                            AddFormControl(inputFieldSettings);
                            break;
                        case DropdownSelectorControlSettingsDescriptor dropdownSettings:
                            AddFormControl(dropdownSettings);
                            break;
                        case FormGroupSettingsDescriptor formGroupSettings:
                            AddFormGroupSettings(formGroupSettings);
                            break;
                        case FormGroupArraySettingsDescriptor formGroupArraySettings:
                            AddFormGroupArray(formGroupArraySettings);
                            break;
                        case FormGroupBoxSettingsDescriptor groupBoxSettingsDescriptor:
                            AddGroupBoxSettings(groupBoxSettingsDescriptor);
                            break;
                        default:
                            throw new ArgumentException($"{nameof(setting)}: B024F65A-50DC-4D45-B8F0-9EC0BE0E2FE2");
                    }
                }
            );
        }

        protected virtual void AddGroupBoxSettings(FormGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is FormGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: B11BFDCD-9612-4584-A420-8FE511A8B64A");

            collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                setting,
                this.ValidationMessages,
                this.formLayout,
                this.parentName
            ).CreateFields();
        }

        protected string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";

        private void AddFormGroupSettings(FormGroupSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.ModelType);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 4817E6BF-0B48-4829-BAB8-7AD17E006EA7");

            switch (setting.FormGroupTemplate.TemplateName)
            {
                case FromGroupTemplateNames.InlineFormGroupTemplate:
                    AddFormGroupInline(setting);
                    break;
                case FromGroupTemplateNames.PopupFormGroupTemplate:
                    AddFormGroupPopup(setting);
                    break;
                default:
                    throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 6664DF64-DF69-415E-8AD2-2AEFC3FA4261");
            }
        }

        private void AddFormGroupPopup(FormGroupSettingsDescriptor setting)
        {
            formLayout.Add(CreateFormValidatableObject(setting), this.groupBoxSettings);
        }

        protected virtual void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            collectionBuilderFactory.GetFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                 this.groupBoxSettings,
                setting.ValidationMessages,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }

        protected virtual void AddFormControl(InputFieldControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);
            AddTextControl(setting, setting.TextTemplate);
        }

        protected virtual void AddFormControl(DropdownSelectorControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);
            AddDropdownControl(setting);
        }

        protected void AddTextControl(InputFieldControlSettingsDescriptor setting, TextFieldTemplateDescriptor textTemplate)
        {
            if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.TextTemplate)
                || textTemplate.TemplateName == nameof(QuestionTemplateSelector.PasswordTemplate))
            {
                formLayout.Add
                (
                    CreateEntryValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Placeholder,
                        setting.StringFormat
                    ),
                    this.groupBoxSettings
                );
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.DateTemplate))
            {
                formLayout.Add(CreateDatePickerValidatableObject(setting, textTemplate.TemplateName), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.HiddenTemplate))
            {
                formLayout.Add(CreateHiddenValidatableObject(setting, textTemplate.TemplateName), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.CheckboxTemplate))
            {
                formLayout.Add(CreateCheckboxValidatableObject(setting, textTemplate.TemplateName, setting.Title), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.SwitchTemplate))
            {
                formLayout.Add(CreateSwitchValidatableObject(setting, textTemplate.TemplateName, setting.Title), this.groupBoxSettings);
            }
            else if (textTemplate.TemplateName == nameof(QuestionTemplateSelector.LabelTemplate))
            {
                formLayout.Add
                (
                    CreateLabelValidatableObject
                    (
                        setting,
                        textTemplate.TemplateName,
                        setting.Title,
                        setting.Placeholder,
                        setting.StringFormat
                    ),
                    this.groupBoxSettings
                );
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.TextTemplate.TemplateName)}: BFCC0C85-244A-4896-BAB2-0D29AD0F86D8");
            }
        }

        protected void AddDropdownControl(DropdownSelectorControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate.TemplateName == nameof(QuestionTemplateSelector.PickerTemplate))
            {
                formLayout.Add(CreatePickerValidatableObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.DropDownTemplate.TemplateName)}: 8A0325D9-E9B0-487D-B569-7E92CDBD4F30");
            }
        }

        private void AddFormGroupArray(FormGroupArraySettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.FormGroupTemplate == null
                || string.IsNullOrEmpty(setting.FormGroupTemplate.TemplateName))
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 0B1F7121-915F-48B9-96A3-B410A67E6853");

            if (setting.FormGroupTemplate.TemplateName == nameof(QuestionTemplateSelector.FormGroupArrayTemplate))
            {
                formLayout.Add(CreateFormArrayValidatableObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.FormGroupTemplate)}: 5E4E494A-E3FE-4016-ABB3-F238DC8E72F9");
            }
        }

        private void AddMultiSelectControl(MultiSelectFormControlSettingsDescriptor setting)
        {
            ValidateSettingType(GetFieldName(setting.Field), setting.Type);

            if (setting.MultiSelectTemplate.TemplateName == nameof(QuestionTemplateSelector.MultiSelectTemplate))
            {
                formLayout.Add(CreateMultiSelectValidatableObject(setting), this.groupBoxSettings);
            }
            else
            {
                throw new ArgumentException($"{nameof(setting.MultiSelectTemplate.TemplateName)}: 880DF2E6-97E8-49F2-B88C-FE8DB4F01C63");
            }
        }

        private IValidatable CreateFormValidatableObject(FormGroupSettingsDescriptor setting) 
            => this.validatableFactory.CreateFormValidatableObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{7E79347E-F11F-48B2-8365-49E98A1061EA}}"),
                GetFieldName(setting.Field),
                nameof(FormValidatableObject<>),
                setting,
                []
            );

        private IValidatable CreateHiddenValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{F69C7063-5F25-408F-9914-08E29245D98C}}");
            IValidatable hiddenValidatable = validatableFactory.CreateHiddenValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                GetValidationRules(setting)
            );

            hiddenValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            return hiddenValidatable;
        }

        private IValidatable CreateCheckboxValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName, string title)
        {
            IValidatable checkboxValidatable = validatableFactory.CreateCheckboxValidatableObject
            (
                GetFieldName(setting.Field),
                templateName,
                title,
                GetValidationRules(setting)
            );

            checkboxValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, typeof(bool));
            return checkboxValidatable;
        }

        private IValidatable CreateSwitchValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName, string title)
        {
            IValidatable switchValidatable = validatableFactory.CreateSwitchValidatableObject
            (
                GetFieldName(setting.Field),
                templateName,
                title,
                GetValidationRules(setting)
            );

            switchValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, typeof(bool));
            return switchValidatable;
        }

        private IValidatable CreateEntryValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName, string placeholder, string stringFormat)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{0CF6F381-C03C-46BE-B3A2-815E8C34B96A}}");
            IValidatable entryValidatable = validatableFactory.CreateEntryValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                placeholder,
                stringFormat,
                GetValidationRules(setting)
            );

            entryValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            return entryValidatable;
        }

        private IValidatable CreateLabelValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName, string title, string placeholder, string stringFormat)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{B0097578-8375-4D4F-B933-DB5708BF1A23}}");
            IValidatable labelValidatable = validatableFactory.CreateLabelValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                title,
                placeholder,
                stringFormat,
                GetValidationRules(setting)
            );

            labelValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            return labelValidatable;
        }

        private IValidatable CreateDatePickerValidatableObject(InputFieldControlSettingsDescriptor setting, string templateName)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{036F8A43-704B-4187-AABA-142B14734582}}");
            IValidatable datePickerValidatable = validatableFactory.CreateDatePickerValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                templateName,
                GetValidationRules(setting)
            );

            datePickerValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            return datePickerValidatable;
        }
        //(DropdownSelectorControlSettingsDescriptor
        private IValidatable CreatePickerValidatableObject(DropdownSelectorControlSettingsDescriptor setting)
        {
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{8FB0D8B8-5273-48AC-98F4-6CE6FC4C81CC}}");
            object? defaultValue = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            IValidatable pickerValidatable = validatableFactory.CreatePickerValidatableObject
            (
                fieldType,
                GetFieldName(setting.Field),
                defaultValue,
                setting.DropDownTemplate,
                GetValidationRules(setting)
            );

            pickerValidatable.Value = defaultValue;
            return pickerValidatable;
        }

        private IValidatable CreateMultiSelectValidatableObject(MultiSelectFormControlSettingsDescriptor setting)
        {
            Type elementType = Type.GetType(setting.MultiSelectTemplate.ModelType) ?? throw new ArgumentException($"{setting.MultiSelectTemplate.ModelType}: {{18D5D086-FC25-4BAF-AE4A-8DB037F7CD12}}");
            Type fieldType = Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{BEA37953-899A-4569-82DA-CAC9A0F6D6C9}}");

            IValidatable multiSelectValidatable = validatableFactory.CreateMultiSelectValidatableObject
            (
                elementType,
                GetFieldName(setting.Field),
                setting,
                GetValidationRules(setting)
            );

            multiSelectValidatable.Value = validatableValueHelper.GetDefaultValue(setting.ValidationSetting, fieldType);
            return multiSelectValidatable;
        }

        private IValidatable CreateFormArrayValidatableObject(FormGroupArraySettingsDescriptor setting) 
            => validatableFactory.CreateFormArrayValidatableObject
            (
                Type.GetType(setting.ModelType) ?? throw new ArgumentException($"{setting.ModelType}: {{18D5D086-FC25-4BAF-AE4A-8DB037F7CD12}}"),
                GetFieldName(setting.Field),
                setting,
                []
            );

        private IValidationRule[]? GetValidationRules(InputFieldControlSettingsDescriptor setting)
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting.Field, setting.Type, setting.ValidationSetting?.DefaultValue)
            ).ToArray();

        private IValidationRule[]? GetValidationRules(DropdownSelectorControlSettingsDescriptor setting)
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting.Field, setting.Type, setting.ValidationSetting?.DefaultValue)
            ).ToArray();

        private IValidationRule[]? GetValidationRules(MultiSelectFormControlSettingsDescriptor setting)
            => setting.ValidationSetting?.Validators?.Select
            (
                validator => GetValidatorRule(validator, setting.Field, setting.Type, setting.ValidationSetting?.DefaultValue)
            ).ToArray();

        private IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, string field, string fieldType, object? defaultValue)
        {
            MethodInfo openGenericMethod = typeof(ValidatorRuleFactory).GetMethod
            (
                nameof(GetValidatorRule),
                1,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                [
                    typeof(ValidatorDefinitionDescriptor),
                    typeof(string),
                    typeof(object),
                    typeof(Dictionary<string, List<ValidationRuleDescriptor>>),
                    typeof(ObservableCollection<IValidatable>)
                ],
                null
            ) ?? throw new InvalidOperationException("{169A5D81-BE55-4017-9BC5-A148F562B5FF}");

            MethodInfo closedGenericMethod = openGenericMethod.MakeGenericMethod(Type.GetType(fieldType) ?? throw new InvalidOperationException("{ED000BF8-9CD9-43DC-991F-C74E1C094C7E}"));

            return (IValidationRule)(closedGenericMethod.Invoke(new ValidatorRuleFactory(this.parentName), [validator, field, defaultValue, this.ValidationMessages, formLayout.Properties]) ?? throw new InvalidOperationException("{CD65E741-5C5D-4BCD-B839-4C3F3FB00E71}"));
        }

        private void ValidateSettingType(string fullPropertyName, string settingFieldType)
        {
            Type fieldType = Type.GetType(settingFieldType) ?? throw new ArgumentException($"{settingFieldType}: F2C4D277-EB70-4C2B-BB42-6A4FC404314D");
            if (!Utils.TypeHelpers.AssignableFrom(GetModelFieldType(fullPropertyName), fieldType))
                throw new ArgumentException($"{nameof(settingFieldType)}: {{7E7749CE-96C1-4EE1-8705-75541CD7D2D8}}");

            Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
        }
    }
}
