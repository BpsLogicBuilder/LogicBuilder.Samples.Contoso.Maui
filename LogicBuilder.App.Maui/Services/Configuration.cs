using LogicBuilder.App.Maui;
using LogicBuilder.App.Maui.Converters;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Services;

#pragma warning disable IDE0130 // Microsoft recommended for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended for service registrations
{
    public static class Configuration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                    .AddSingleton<IUiNotificationService, UiNotificationService>()
                    .AddSingleton<ICollectionCellManager, CollectionCellManager>()
                    .AddSingleton<IEntityStateUpdater, EntityStateUpdater>()
                    .AddSingleton<IEntityUpdater, EntityUpdater>()
                    .AddSingleton<IPropertiesUpdater, PropertiesUpdater>()
                    .AddSingleton<IReadOnlyPropertiesUpdater, ReadOnlyPropertiesUpdater>()
                    .AddSingleton<IReadOnlyCollectionCellPropertiesUpdater, ReadOnlyCollectionCellPropertiesUpdater>()
                    .AddAutoMapperServices()
                    .AddHttpClient()
                    .AddSingleton<IHttpService, HttpService>()
                    .AddSingleton<IConverterFactory, ConverterFactory>()
                    .AddAppUtilsServices()
                    .AddTransient<IUrlService, UrlService>()
                    .AddTransient<IPagingSelectorBuilder, PagingSelectorBuilder>()/*must be transient - should not create scoped service from a singleton context*/
                    .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>();
        }
    }
}
