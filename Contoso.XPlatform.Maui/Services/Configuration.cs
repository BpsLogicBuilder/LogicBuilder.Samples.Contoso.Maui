using Contoso.XPlatform;
using Contoso.XPlatform.Converters;
using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Services;
using LogicBuilder.App.Utils.Rules;
using LogicBuilder.RulesDirector;

#pragma warning disable IDE0130 // Microsoft recommended for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended for service registrations
{
    internal static class Configuration
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
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
                    .AddTransient<IPagingSelectorBuilder, PagingSelectorBuilder>()/*must be transient - should not create scoped service from a singleton context*/
                    .AddTransient<IScopedFlowManagerService, ScopedFlowManagerService>();
        }

        internal static IServiceCollection AddRulesCache(this IServiceCollection services)
        {
            return services.AddRulesCacheService
            (
                new RulesLoaderRequest
                (
                    "Contoso.XPlatform.Flow.Rulesets",
                    typeof(FlowActivity),
                    [
                        typeof(LogicBuilder.App.Utils.Interfaces.ITypeHelper).Assembly,
                        typeof(LogicBuilder.App.Maui.Forms.Parameters.CommandButtonParameters).Assembly,
                        typeof(LogicBuilder.App.Maui.Forms.Configuration.CommandButtonDescriptor).Assembly,
                        typeof(LogicBuilder.Forms.Parameters.ConnectorParameters).Assembly,
                        typeof(Contoso.Domain.Entities.StudentModel).Assembly,
                        typeof(Contoso.Data.Entities.Course).Assembly,
                        typeof(DirectorBase).Assembly,
                        typeof(string).Assembly
                    ]
                )
            );
        }
    }
}
