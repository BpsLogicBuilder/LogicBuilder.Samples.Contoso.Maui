using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.ViewModels.Factories;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.ViewModels.Validatables.Factories;
using LogicBuilder.App.Maui.Forms.Configuration.DataForm;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.App.Maui.Services
{
    public class UpdateOnlyFieldsCollectionBuilder(
        ICollectionBuilderFactory collectionBuilderFactory,
        IValidatableFactory validatableFactory,
        IValidatableValueHelper validatableValueHelper,
        List<IFormItemSettingsDescriptor> fieldSettings,
        IFormGroupBoxSettings groupBoxSettings,
        Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
        Type modelType,
        EditFormLayout? formLayout = null,
        string? parentName = null) : FieldsCollectionBuilder(
            collectionBuilderFactory,
            validatableFactory,
            validatableValueHelper,
            fieldSettings,
            groupBoxSettings,
            validationMessages,
            modelType,
            formLayout,
            parentName), IUpdateOnlyFieldsCollectionBuilder
    {
        protected override void AddFormControl(InputFieldControlSettingsDescriptor setting)
        {
            if (setting.UpdateOnlyTextTemplate != null)
            {
                AddTextControl(setting, setting.UpdateOnlyTextTemplate);
            }
            else if (setting.TextTemplate != null)
                AddTextControl(setting, setting.TextTemplate);
            else
                throw new ArgumentException($"{nameof(setting)}: {{AB8B9944-1F94-4E32-B2D5-9FD7F459D233}}");
        }

        protected override void AddFormControl(DropdownSelectorControlSettingsDescriptor setting)
        {
            if (setting.DropDownTemplate != null)
                AddDropdownControl(setting);
            else
                throw new ArgumentException($"{nameof(setting)}: {{C4FF2F40-B2F2-4D13-9565-AD97377AAE3F}}");
        }

        protected override void AddGroupBoxSettings(FormGroupBoxSettingsDescriptor setting)
        {
            this.formLayout.AddControlGroupBox(setting);

            if (setting.FieldSettings.Any(s => s is FormGroupBoxSettingsDescriptor))
                throw new ArgumentException($"{nameof(setting.FieldSettings)}: 25BFA228-1B14-4B32-AA1C-8F8002DF4413");

            collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                setting,
                this.ValidationMessages,
                this.formLayout,
                 this.parentName
            ).CreateFields();
        }

        protected override void AddFormGroupInline(FormGroupSettingsDescriptor setting)
        {
            collectionBuilderFactory.GetUpdateOnlyFieldsCollectionBuilder
            (
                this.modelType,
                setting.FieldSettings,
                this.groupBoxSettings,
                setting.ValidationMessages,
                this.formLayout,
                GetFieldName(setting.Field)
            ).CreateFields();
        }
    }
}
