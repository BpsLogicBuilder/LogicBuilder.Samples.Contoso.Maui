using LogicBuilder.App.Utils.Interfaces;
using Microsoft.Maui.Controls;

namespace LogicBuilder.App.Maui.Converters
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
