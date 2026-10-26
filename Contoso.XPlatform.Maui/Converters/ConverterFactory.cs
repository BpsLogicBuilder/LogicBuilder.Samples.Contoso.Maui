using LogicBuilder.App.Utils.Interfaces;
using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Converters
{
    public class ConverterFactory(ITypeHelper typeHelper, IGenericsHelpers genericsHelpers) : IConverterFactory
    {
        private readonly ITypeHelper typeHelper = typeHelper;
        private readonly IGenericsHelpers genericsHelpers = genericsHelpers;

        public IValueConverter CreateStringFormatConverter()
            => new StringFormatConverter(typeHelper);

        public IValueConverter CreatePickerItemDisplayPathConverter()
            => new PickerItemDisplayPathConverter(genericsHelpers);
    }
}
