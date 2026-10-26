using Contoso.XPlatform.Validators;
using System.Collections.Generic;

namespace Contoso.XPlatform.ViewModels.Validatables
{
    public partial class HiddenValidatableObject<T>(IUiNotificationService uiNotificationService, string name, string templateName, IEnumerable<IValidationRule>? validations) : ValidatableObjectBase<T>(name, templateName, validations, uiNotificationService)
    {
    }
}
