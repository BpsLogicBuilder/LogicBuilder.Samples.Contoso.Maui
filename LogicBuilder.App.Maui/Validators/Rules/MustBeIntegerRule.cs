using Contoso.XPlatform.ViewModels.Validatables;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record MustBeIntegerRule<T> : ValidationRuleBase<T>
    {
        public MustBeIntegerRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties) : base(fieldName, validationMessage, allProperties)
        {
        }

        public override string ClassName => nameof(MustBeIntegerRule<>);

        public override bool Check() 
            => Value is null || int.TryParse(Value.ToString(), out _);
    }
}
