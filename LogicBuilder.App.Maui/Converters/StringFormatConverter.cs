using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Utils.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace LogicBuilder.App.Maui.Converters
{
    public class StringFormatConverter(ITypeHelper typeHelper) : IValueConverter
    {
        private readonly ITypeHelper typeHelper = typeHelper;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return GetFormattedString(GetStringFormat());

            object GetFormattedString(string? stringFormat)
            {
                if (value == null)
                    return null!;/*ok to return null for converters*/

                if (string.IsNullOrEmpty(stringFormat))
                    return value;

                return string.Format(CultureInfo.CurrentCulture, stringFormat, value);
            }

            string? GetStringFormat()
            {
                return (((VisualElement)parameter!).BindingContext as IHasStringFormat)?.StringFormat;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (typeHelper.TryParse((value ?? "").ToString()!, targetType, out object? result))
                return result;

            return value;
        }
    }
}
