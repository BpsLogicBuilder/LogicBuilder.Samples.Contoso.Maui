using Contoso.XPlatform.Views;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    internal static class ViewServices
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            return services
                .AddTransient<ExtendedSplashView>();
        }
    }
}
