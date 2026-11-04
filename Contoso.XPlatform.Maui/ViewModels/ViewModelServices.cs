using Contoso.XPlatform.ViewModels;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    internal static class ViewModelServices
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
            => services
                .AddTransient<ExtendedSplashViewModel>();
    }
}
