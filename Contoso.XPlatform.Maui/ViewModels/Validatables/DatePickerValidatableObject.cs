using Contoso.XPlatform.Validators;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public partial class DatePickerValidatableObject<T>(IUiNotificationService uiNotificationService, string name, string templateName, IEnumerable<IValidationRule>? validations) : ValidatableObjectBase<T>(name, templateName, validations, uiNotificationService)
    {
        public ICommand DateChangedCommand => new Command
        (
            () =>
            {
                IsDirty = true;
                IsValid = Validate();
            }
        );
    }
}
