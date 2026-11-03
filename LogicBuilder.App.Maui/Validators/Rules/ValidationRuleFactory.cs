using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Contoso.XPlatform.Validators.Rules
{
    internal class ValidationRuleFactory(string? parentName)
    {
        private readonly string? parentName = parentName;

        public IValidationRule GetValidatorRule<T>(
            ValidatorDefinitionDescriptor validator,
            string field,
            object? defaultValue,
            Dictionary<string, List<ValidationRuleDescriptor>> validationMessages,
            ObservableCollection<IValidatable> fields)
        {
            if (!validationMessages.TryGetValue(field, out List<ValidationRuleDescriptor>? methodList))
                throw new ArgumentException($"{nameof(field)}: 4FF12AAC-DF7F-4346-8747-52413FCA808F");

            Dictionary<string, string> methodDictionary = methodList.ToDictionary(vr => vr.ClassName, vr => vr.Message);

            if (!methodDictionary.TryGetValue(validator.ClassName, out string? validationMessage))
                throw new ArgumentException($"{nameof(validator.ClassName)}: 8A45F637-347D-4578-9F9C-72E9026FBCEB");

            if (validator.ClassName == nameof(RequiredRule<>))
                return GetRequiredRule<T>(field, validationMessage, fields, defaultValue);
            else if (validator.ClassName == nameof(IsMatchRule<>))
                return GetIsMatchRule<T>(field, validationMessage, fields, validator);
            else if (validator.ClassName == nameof(RangeRule<>))
                return GetRangeRule<T>(field, validationMessage, fields, validator);
            else if (validator.ClassName == nameof(MustBeNumberRule<>))
                return GetMustBeNumberRule<T>(field, validationMessage, fields);
            else if (validator.ClassName == nameof(MustBePositiveNumberRule<>))
                return GetMustBePositiveNumberRule<T>(field, validationMessage, fields);
            else if (validator.ClassName == nameof(MustBeIntegerRule<>))
                return GetMustBeIntegerRule<T>(field, validationMessage, fields);
            else if (validator.ClassName == nameof(IsLengthValidRule))
                return GetIsLengthValidRule(field, validationMessage, fields, validator);
            else if (validator.ClassName == nameof(IsValidEmailRule))
                return GetIsValidEmailRule(field, validationMessage, fields);
            else if (validator.ClassName == nameof(IsValidPasswordRule))
                return GetIsValidPasswordRule(field, validationMessage, fields);
            else if (validator.ClassName == nameof(IsPatternMatchRule))
                return GetIsPatternMatchRule(field, validationMessage, fields, validator);
            else if (validator.ClassName == nameof(IsValueTrueRule))
                return GetIsValueTrueRule(field, validationMessage, fields);
            else if (validator.ClassName == nameof(AtLeastOneRequiredRule<>))
                return GetAtLeastOneRequiredRule<T>(field, validationMessage, fields);
            else
                throw new ArgumentException($"{nameof(validator.ClassName)}: CF4FDB4D-F135-40E0-BB31-14DBA624FC25");
        }

        MustBePositiveNumberRule<T> GetMustBePositiveNumberRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields)
        => new
        (
            GetFieldName(field),
            validationMessage,
            fields
        );

        MustBeNumberRule<T> GetMustBeNumberRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields)
            => new
            (
                GetFieldName(field),
                validationMessage,
                fields
            );

        MustBeIntegerRule<T> GetMustBeIntegerRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields)
            => new
            (
                GetFieldName(field),
                validationMessage,
                fields
            );

        RequiredRule<T> GetRequiredRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields, object? defaultValue)
        {
            if (defaultValue != null
                && !Utils.TypeHelpers.AssignableFrom(typeof(T), defaultValue.GetType()))
                throw new ArgumentException($"{nameof(defaultValue)}: C96394B8-B26B-45B2-8C34-B9BA3FF95088");

            return new RequiredRule<T>
            (
                GetFieldName(field),
                validationMessage,
                fields,
                defaultValue == null ? default : (T)defaultValue
            );
        }

        IsPatternMatchRule GetIsPatternMatchRule(string field, string validationMessage, ObservableCollection<IValidatable> fields, ValidatorDefinitionDescriptor validator)
        {
            if (validator.Arguments == null)
                throw new InvalidOperationException("{0D6B850E-2EA7-4F83-8ABE-8732E6DBB17D}");

            const string pattern = "pattern";
            if (!validator.Arguments.TryGetValue(pattern, out ValidatorArgumentDescriptor? patternDescriptor))
                throw new ArgumentException($"{pattern}: 086E280E-03C7-4900-A8DB-2C570CEEC91A");

            return new IsPatternMatchRule
            (
                GetFieldName(field),
                validationMessage,
                fields,
                (string)patternDescriptor.Value
            );
        }

        IsLengthValidRule GetIsLengthValidRule(string field, string validationMessage, ObservableCollection<IValidatable> fields, ValidatorDefinitionDescriptor validator)
        {
            if (validator.Arguments == null)
                throw new InvalidOperationException("{FF67CBE8-E3C1-4C09-91A4-2065B6A32047}");

            const string argumentMin = "minimunLength";
            const string argumentMax = "maximunLength";
            if (!validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor? minDescriptor))
                throw new ArgumentException($"{argumentMin}: 521CBE54-0677-4633-AB4F-35A355490D89");
            if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor? maxDescriptor))
                throw new ArgumentException($"{argumentMax}: EEB2EC10-42B9-49EC-A7A3-86530D11C679");

            return new IsLengthValidRule
            (
                GetFieldName(field),
                validationMessage,
                fields,
                (int)minDescriptor.Value,
                (int)maxDescriptor.Value
            );
        }

        IsMatchRule<T> GetIsMatchRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields, ValidatorDefinitionDescriptor validator)
        {
            const string argumentName = "otherFieldName";
            if (validator.Arguments == null || !validator.Arguments.TryGetValue(argumentName, out ValidatorArgumentDescriptor? validatorArgumentDescriptor))
                throw new ArgumentException($"{argumentName}: ADB88D64-F9DA-4FC0-B9C0-CB910F86B735");

            return new IsMatchRule<T>
            (
                GetFieldName(field),
                validationMessage,
                fields,
                GetFieldName((string)validatorArgumentDescriptor.Value)
            );
        }

        IsValueTrueRule GetIsValueTrueRule(string field, string validationMessage, ObservableCollection<IValidatable> fields)
            => new
            (
                GetFieldName(field),
                validationMessage,
                fields
            );

        IsValidEmailRule GetIsValidPasswordRule(string field, string validationMessage, ObservableCollection<IValidatable> fields)
            => new
            (
                GetFieldName(field),
                validationMessage,
                fields
            );

        IsValidEmailRule GetIsValidEmailRule(string field, string validationMessage, ObservableCollection<IValidatable> fields)
            => new
            (
                GetFieldName(field),
                validationMessage,
                fields
            );

        private RangeRule<T> GetRangeRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields, ValidatorDefinitionDescriptor validator)
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("Type must implement IComparable of T.");

            const string argumentMin = "min";
            const string argumentMax = "max";
            if (validator.Arguments == null || !validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor? minDescriptor))
                throw new ArgumentException($"{argumentMin}: 34965468-76E0-4FA0-A3EC-16F2BCCB2CE0");
            if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor? maxDescriptor))
                throw new ArgumentException($"{argumentMax}: 6AA3A056-3ECA-4F48-B79A-A326B2188D14");

            return new RangeRule<T>
            (
                GetFieldName(field),
                validationMessage,
                fields,
                (IComparable<T>)minDescriptor.Value,
                (IComparable<T>)maxDescriptor.Value
            );
        }

        private IValidationRule GetAtLeastOneRequiredRule<T>(string field, string validationMessage, ObservableCollection<IValidatable> fields)
        {
            if (!typeof(IEnumerable<object>).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("Type must be IEnumerable<object>.");

            //return new AtLeastOneRequiredRule<T>(GetFieldName(field), validationMessage, fields);
            return (IValidationRule)(
                Activator.CreateInstance
                (
                    typeof(AtLeastOneRequiredRule<>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType
                        (
                            typeof(T).GetGenericArguments()[0]
                        )
                    ),
                    [
                        GetFieldName(field),
                        validationMessage,
                        fields
                    ]
                ) ?? throw new ArgumentException($"{nameof(AtLeastOneRequiredRule<>)}: {{824A0081-0E2F-4929-8BF2-C1F65D54AC6D}}")
            );
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
