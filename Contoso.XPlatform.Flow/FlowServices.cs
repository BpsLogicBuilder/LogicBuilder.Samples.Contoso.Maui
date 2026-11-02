#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    public static class FlowServices
    {
        public static IServiceCollection AddFlowServices(this IServiceCollection services)
            => services
                .AddMauiUtilsServices()
                .AddFlowFactories();
    }
}
