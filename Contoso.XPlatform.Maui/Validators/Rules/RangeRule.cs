using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Validators.Rules
{
    public record RangeRule<T> : ValidationRuleBase<T>
    {
        public RangeRule(string fieldName, string validationMessage, ICollection<IValidatable> allProperties, IComparable<T> min, IComparable<T> max)
            : base(fieldName, validationMessage, allProperties)
        {
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                throw new InvalidOperationException("Type must implement IComparable of T.");

            Min = min;
            Max = max;
        }

        public override string ClassName => nameof(RangeRule<>);
        public IComparable<T> Min { get; }
        public IComparable<T> Max { get; }

        public override bool Check()
        {
            if (Min.CompareTo(Value) > 0)
                return false;

            if (Max.CompareTo(Value) < 0)
                return false;

            return true;
        }
    }
}
