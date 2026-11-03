using AutoMapper;
using LogicBuilder.App.Maui.MappingProfiles;
using LogicBuilder.App.Maui.AutoMapperProfiles;
using LogicBuilder.EntityFrameworkCore.Mapping;
using Microsoft.Extensions.Logging.Abstractions;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
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
