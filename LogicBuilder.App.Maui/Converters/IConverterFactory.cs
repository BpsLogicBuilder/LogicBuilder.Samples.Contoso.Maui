using Microsoft.Maui.Controls;

namespace LogicBuilder.App.Maui.Converters
{
    public interface IConverterFactory
    {
        IValueConverter CreateStringFormatConverter();
        IValueConverter CreatePickerItemDisplayPathConverter();
    }
}
