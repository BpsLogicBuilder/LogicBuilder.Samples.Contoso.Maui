using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public interface IValidatableValueHelper
    {
        object? GetDefaultValue(FieldValidationSettingsDescriptor? validationSetting, Type type);
    }
}
