using Contoso.XPlatform.Flow;
using Contoso.XPlatform.Flow.Cache;
using LogicBuilder.RulesDirector;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    public static class FlowServices
    {
        public static IServiceCollection AddFlowServices(this IServiceCollection services)
            => services
                .AddScoped<IFlowManager, FlowManager>()
                .AddScoped<FlowDataCache>()
                .AddScoped<ScreenData>()
                .AddScoped<Progress>()
                .AddScoped<IDialogFunctions, DialogFunctions>()
                .AddScoped<IActions, Actions>()
                .AddFlowFactories();
    }
}
