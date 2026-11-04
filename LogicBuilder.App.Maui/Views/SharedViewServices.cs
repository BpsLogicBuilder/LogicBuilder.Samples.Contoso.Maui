using LogicBuilder.App.Maui.Converters;
using LogicBuilder.App.Maui.ViewModels.DetailForm;
using LogicBuilder.App.Maui.ViewModels.EditForm;
using LogicBuilder.App.Maui.ViewModels.ListPage;
using LogicBuilder.App.Maui.ViewModels.ReadOnlys;
using LogicBuilder.App.Maui.ViewModels.SearchPage;
using LogicBuilder.App.Maui.ViewModels.TextPage;
using LogicBuilder.App.Maui.ViewModels.Validatables;
using LogicBuilder.App.Maui.Views;
using LogicBuilder.App.Maui.Views.Factories;
using System;

#pragma warning disable IDE0130 // Microsoft recommended namespace for service registrations
namespace Microsoft.Extensions.DependencyInjection
#pragma warning restore IDE0130 // Microsoft recommended namespace for service registrations
{
    public static class SharedViewServices
    {
        public static IServiceCollection AddSharedViews(this IServiceCollection services)
        {
            return services
                .AddTransient<MainPageView>()
                .AddTransient<Func<IValidatable, ChildFormArrayPageCS>>
                (
                    provider =>
                    validatable => new ChildFormArrayPageCS
                    (
                        validatable
                    )
                )
                .AddTransient<Func<IValidatable, ChildFormPageCS>>
                (
                    provider =>
                    validatable => new ChildFormPageCS
                    (
                        validatable,
                        provider.GetRequiredService<IConverterFactory>()
                    )
                )
                .AddTransient<Func<DetailFormViewModelBase, DetailFormViewCS>>
                (
                    provider =>
                    viewModel => new DetailFormViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<EditFormViewModelBase, EditFormViewCS>>
                (
                    provider =>
                    viewModel => new EditFormViewCS
                    (
                        viewModel,
                        provider.GetRequiredService<IConverterFactory>()
                    )
                )
                .AddTransient<Func<ListPageViewModelBase, ListPageViewCS>>
                (
                    provider =>
                    viewModel => new ListPageViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<IValidatable, MultiSelectPageCS>>
                (
                    provider =>
                    validatable => new MultiSelectPageCS
                    (
                        validatable
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormArrayPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyChildFormArrayPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyChildFormPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyChildFormPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<IReadOnly, ReadOnlyMultiSelectPageCS>>
                (
                    provider =>
                    readOnly => new ReadOnlyMultiSelectPageCS
                    (
                        readOnly
                    )
                )
                .AddTransient<Func<SearchPageViewModelBase, SearchPageViewCS>>
                (
                    provider =>
                    viewModel => new SearchPageViewCS
                    (
                        viewModel
                    )
                )
                .AddTransient<Func<TextPageViewModel, TextPageViewCS>>
                (
                    provider =>
                    viewModel => new TextPageViewCS
                    (
                        viewModel
                    )
                )
                .AddViewFactories();
        }
    }
}
