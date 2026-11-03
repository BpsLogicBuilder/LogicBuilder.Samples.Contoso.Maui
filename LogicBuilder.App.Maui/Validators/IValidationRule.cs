using LogicBuilder.App.Maui.ViewModels.Validatables;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Validators
{
    public interface IValidationRule
    {
        string FieldName { get; }
        string ClassName { get; }
        ICollection<IValidatable> AllProperties { get; set; }
        string ValidationMessage { get; set; }
        bool Check();
    }
}
