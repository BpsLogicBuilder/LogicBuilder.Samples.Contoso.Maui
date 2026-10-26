using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using LogicBuilder.Expressions.Utils;
using System;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    internal class ValidatableValueHelper : IValidatableValueHelper
    {
        private static readonly DateTime DefaultDateTime = new(1900, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

        public object? GetDefaultValue(FieldValidationSettingsDescriptor? validationSetting, Type type)
        {
            if (validationSetting?.DefaultValue != null)
            {
                if (!Utils.TypeHelpers.AssignableFrom(type, validationSetting.DefaultValue.GetType()))
                    throw new ArgumentException($"{nameof(validationSetting.DefaultValue)}: {{974B3DA6-C626-4CE4-AB46-A5EDACBD2CFC}}");

                return validationSetting.DefaultValue;
            }

            if (type == typeof(DateTime))
                return DefaultDateTime;

            return !type.IsValueType || type.IsNullableType()
                ? null
                : Activator.CreateInstance(type);
        }
    }
}
