using LogicBuilder.App.Maui.Validators;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.ViewModels.Validatables
{
    public partial class HiddenValidatableObject<T>(IUiNotificationService uiNotificationService, string name, string templateName, IEnumerable<IValidationRule>? validations) : ValidatableObjectBase<T>(name, templateName, validations, uiNotificationService)
    {
    }
}
