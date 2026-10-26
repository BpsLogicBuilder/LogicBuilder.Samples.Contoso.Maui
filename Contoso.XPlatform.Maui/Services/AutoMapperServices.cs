using AutoMapper;
using Contoso.XPlatform.MappingProfiles;
using LogicBuilder.App.Maui.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.Logging.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class AutoMapperServices
    {
        internal static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            return services.AddSingleton<IConfigurationProvider>
                (
                    new MapperConfiguration(cfg =>
                    {
                        cfg.AddMaps(typeof(ExpressionOperatorsMappingProfile), typeof(CommandButtonProfile), typeof(MenuItemProfile));
                        cfg.AllowNullCollections = true;
                    }, new NullLoggerFactory())
                ).AddTransient<IMapper>
                (
                    sp => new Mapper
                    (
                        sp.GetRequiredService<AutoMapper.IConfigurationProvider>(),
                        sp.GetService
                    )
                );
        }
    }
}
