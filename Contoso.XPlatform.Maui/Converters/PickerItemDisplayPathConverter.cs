using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Utils.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Contoso.XPlatform.Converters
{
    public class PickerItemDisplayPathConverter(IGenericsHelpers genericsHelpers) : IValueConverter
    {
        private readonly IGenericsHelpers genericsHelpers = genericsHelpers;

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null!;/*ok to return null for converters*/

            IHasDropDownTemplate hasDropDownTemplate = (IHasDropDownTemplate)((VisualElement)parameter!).BindingContext;

            return genericsHelpers.GetPropertyValue<string>(value, hasDropDownTemplate.DropDownTemplate.TextField);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
