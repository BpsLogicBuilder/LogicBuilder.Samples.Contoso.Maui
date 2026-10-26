using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.App.Maui.Forms.Configuration;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Contoso.XPlatform.Converters
{
    public class PickerItemDisplayPathConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null)
                return null!;/*ok to return null for converters*/

            object bindingContext = ((VisualElement)parameter!).BindingContext;
            //var dropDownTemplate = bindingContext.GetPropertyValue<DropDownTemplateDescriptor>
            //(
            //    nameof(PickerValidatableObject<int>.DropDownTemplate)
            //);

            //return value.GetPropertyValue<string>(dropDownTemplate.TextField);
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
