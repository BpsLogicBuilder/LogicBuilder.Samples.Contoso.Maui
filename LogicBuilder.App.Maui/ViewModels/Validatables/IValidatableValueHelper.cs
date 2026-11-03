using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;

namespace LogicBuilder.App.Maui.ViewModels.Validatables
{
    public interface IValidatableValueHelper
    {
        object? GetDefaultValue(FieldValidationSettingsDescriptor? validationSetting, Type type);
    }
}
