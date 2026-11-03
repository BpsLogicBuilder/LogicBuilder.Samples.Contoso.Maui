using LogicBuilder.App.Maui.Behaviours;
using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.ViewModels.SearchPage;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.Views
{
    public partial class SearchPageViewCS : ContentPage
    {
        public SearchPageViewCS(SearchPageViewModelBase searchPageViewModel)
        {
            this.SearchPageListViewModel = searchPageViewModel;
            AddContent();
            BindingContext = this.SearchPageListViewModel;
        }

        public SearchPageViewModelBase SearchPageListViewModel { get; set; }
        private Grid transitionGrid;
        private Grid page;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (transitionGrid.IsVisible)
                await page.EntranceTransition(transitionGrid, 150);
        }

        [MemberNotNull(nameof(transitionGrid), nameof(page))]
        private void AddContent()
        {
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.SearchPageListViewModel.Buttons);
            Title = SearchPageListViewModel.FormSettings.Title;

            page = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchPageViewLayoutStyle),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                Children =
                {
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(SearchPageViewModelBase.Title)))
                    .SetGridRow(0),
                    new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                        },
                        Children =
                        {
                            new SearchBar
                            {
                                Behaviors =
                                {
                                    new EventToCommandBehavior
                                    {
                                        EventName = nameof(SearchBar.TextChanged),
                                    }
                                    .AddBinding(EventToCommandBehavior.CommandProperty, new Binding(nameof(SearchPageViewModel<>.TextChangedCommand)))
                                }
                            }
                            .AddBinding(SearchBar.TextProperty, new Binding(nameof(SearchPageViewModel<>.SearchText)))
                            .AddBinding(SearchBar.PlaceholderProperty, new Binding(nameof(SearchPageViewModelBase.FilterPlaceholder))),
#if WINDOWS
                            new Button
                            {
                                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.PullButtonStyle),
                            }
                            .AddBinding(Button.CommandProperty, new Binding(nameof(SearchPageViewModel<>.ButtonRefreshCommand)))
                            .SetGridColumn(1)
#endif
                        }
                    }
                    .SetGridRow(1),
                    new RefreshView
                    {/* RefreshView pulls to the right on iOS https://github.com/dotnet/maui/issues/7315 */
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormRefreshViewStyle),
                        Content = new CollectionView
                        {
                            Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.SearchFormCollectionViewStyle),
                            ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                            (
                                this.SearchPageListViewModel.FormSettings.ItemTemplateName,
                                this.SearchPageListViewModel.FormSettings.Bindings
                            )
                        }
                        .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SearchPageViewModel<>.Items)))
                        .AddBinding(SelectableItemsView.SelectionChangedCommandProperty, new Binding(nameof(SearchPageViewModel<>.SelectionChangedCommand)))
                        .AddBinding(SelectableItemsView.SelectedItemProperty, new Binding(nameof(SearchPageViewModel<>.SelectedItem)))
                    }
                    .AddBinding(RefreshView.IsRefreshingProperty, new Binding(nameof(SearchPageViewModel<>.IsRefreshing)))
                    .AddBinding(RefreshView.CommandProperty, new Binding(nameof(SearchPageViewModel<>.RefreshCommand)))
                    .SetGridRow(2)
                }
            };

            transitionGrid = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.TransitionGridStyle)
            };

            Content = new Grid
            {
                Children =
                {
                    page,
                    transitionGrid
                }
            };
        }
    }
}