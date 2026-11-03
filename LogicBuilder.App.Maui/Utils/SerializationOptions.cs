using LogicBuilder.App.Bsl.Business.Responses.Json;
using LogicBuilder.App.Utils.Json;
using LogicBuilder.Domain.Json;
using LogicBuilder.Expressions.Utils.Json;
using System.Text.Json;

namespace LogicBuilder.App.Maui.Utils
{
    public static class SerializationOptions
    {
        private static JsonSerializerOptions? _default;
        public static JsonSerializerOptions Default
        {
            get
            {
                if (_default != null)
                    return _default;

                _default = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters =
                    {
                        new DescriptorConverter(),
                        new ModelConverter(),
                        new ObjectConverter(),
                        new ResponseConverter()
                    }
                };

                return _default;
            }
        }
    }
}
