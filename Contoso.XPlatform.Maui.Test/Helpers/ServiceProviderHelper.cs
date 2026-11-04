using Contoso.XPlatform.Maui.Test.Mocks;
using LogicBuilder.App.Maui.Services;
using LogicBuilder.App.Maui.Utils.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Contoso.XPlatform.Maui.Test.Helpers
{
    internal static class ServiceProviderHelper
    {
        private static readonly IServiceProvider serviceProvider = CreateServiceProvider();

        static ServiceProvider CreateServiceProvider()
        {
            ServiceCollection serviceCollection = [];
            serviceCollection
                .AddLogicBuilderMauiServices()
                .AddFlowServices()
                .AddRulesCache()
                .AddViewModels()
                .AddViews();

            ServiceCollection services = [];
            HashSet<Type> types = [];
            foreach (var service in serviceCollection)
            {
                if (service.ServiceType == typeof(IHttpService))
                    services.Add(new ServiceDescriptor(service.ServiceType, typeof(HttpServiceMock), service.Lifetime));
                else if (service.ServiceType == typeof(IAppLogger))
                    services.Add(new ServiceDescriptor(service.ServiceType, typeof(AppLoggerMock), service.Lifetime));
                else
                    services.Add(service);

                types.Add(service.ServiceType);
            }

            if (!types.Contains(typeof(IAppLogger)))
                services.Add(new ServiceDescriptor(typeof(IAppLogger), typeof(AppLoggerMock), ServiceLifetime.Singleton));

            if (!types.Contains(typeof(IUpdateOnlyFieldsCollectionBuilder)))
                services.Add(new ServiceDescriptor(typeof(IUpdateOnlyFieldsCollectionBuilder), typeof(UpdateOnlyFieldsCollectionBuilder), ServiceLifetime.Singleton));

            return services.BuildServiceProvider();
        }

        internal static IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
