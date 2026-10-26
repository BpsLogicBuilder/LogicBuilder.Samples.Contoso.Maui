using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Converters
{
    public interface IConverterFactory
    {
        IValueConverter CreateStringFormatConverter();
        IValueConverter CreatePickerItemDisplayPathConverter();
    }
}
