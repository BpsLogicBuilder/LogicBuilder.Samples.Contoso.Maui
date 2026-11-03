using LogicBuilder.App.Maui.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.App.Maui.Validators.Rules
{
    public record AtLeastOneRequiredRule<T> : ValidationRuleBase<T>
    {
        public AtLeastOneRequiredRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties)
            : base(fieldName, validationMessage, allProperties)
        {
            if (!typeof(IEnumerable<object>).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("Type must be IEnumerable<object>.");
        }

        public override string ClassName { get => nameof(AtLeastOneRequiredRule<>); }

        public override bool Check()
        {
            if (Value is null)
                return false;

            return ((IEnumerable<object>)Value).Any();
        }
    }
}
