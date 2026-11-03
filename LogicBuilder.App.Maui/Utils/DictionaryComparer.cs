using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicBuilder.App.Maui.Utils
{
    public class DictionaryComparer : IEqualityComparer<Dictionary<string, object?>>
    {
        public bool Equals(Dictionary<string, object?>? x, Dictionary<string, object?>? y)
        {
            if (x == null && y == null)
                return true;

            if (x == null ^ y == null)
                return false;

            if (x!.Count != y!.Count)/*neither can be null at this point*/
                return false;

            if (x.Keys.Except(y.Keys).Any())
                return false;

            if (y.Keys.Except(x.Keys).Any())
                return false;

            if(x.Any(pair => ValuesDifferent(y, pair)))
                return false;

            return true;
        }

        private static bool ValuesDifferent(Dictionary<string, object?> y, KeyValuePair<string, object?> pair)
        {
            if (pair.Value == null)
            {
                return y[pair.Key] != null;
            }
            else if (pair.Value.GetType().IsLiteralType())
            {
                if (!pair.Value.Equals(y[pair.Key]))
                    return true;
            }
            else if (pair.Value.GetType() == typeof(Dictionary<string, object?>))
            {
                if (!new DictionaryComparer().Equals((Dictionary<string, object?>)pair.Value, (Dictionary<string, object?>?)y[pair.Key]))
                    return true;
            }
            else if (pair.Value is IEnumerable<Dictionary<string, object?>> enumerable)
            {
                var xToHashSet = enumerable
                    .ToHashSet(new DictionaryComparer());

                if (!xToHashSet.SetEquals((IEnumerable<Dictionary<string, object?>>)(y[pair.Key] ?? new List<Dictionary<string, object?>>())))
                    return true;
            }
            else if (pair.Value.GetType().IsList())
            {
                Type pairType = pair.Value.GetType();
                Type elementType = pairType.GetUnderlyingElementType();
                if (elementType.IsLiteralType()
                    && !ListHelper.ListEquals(elementType, (System.Collections.IEnumerable)pair.Value, (System.Collections.IEnumerable)(y[pair.Key] ?? new List<object>())))
                {
                    return true;
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(pair)}: E7AFB1A2-E3A9-4EC0-9AAD-99E368A76870");
            }

            return false;
        }

        public int GetHashCode(Dictionary<string, object?> obj)
        {
            if (obj == null)
                return string.Empty.GetHashCode();

            return string.Join
            (
                "|", 
                obj.Keys.OrderBy(key => key)
            ).GetHashCode();
        }
    }
}
