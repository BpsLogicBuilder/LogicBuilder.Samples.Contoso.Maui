using LogicBuilder.App.Maui.ViewModels.Validatables;
using System.Collections.Generic;

namespace LogicBuilder.App.Maui.Validators.Rules
{
    public record IsValueTrueRule : ValidationRuleBase<bool>
    {
        public IsValueTrueRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName { get => nameof(IsValueTrueRule); }
        public override bool Check() => Value;
    }
}
