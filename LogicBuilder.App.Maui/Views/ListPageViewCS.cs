using LogicBuilder.App.Maui.Constants;
using LogicBuilder.App.Maui.Utils;
using LogicBuilder.App.Maui.ViewModels.ListPage;

using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics.CodeAnalysis;

namespace LogicBuilder.App.Maui.Views
{
    public partial class ListPageViewCS : ContentPage
    {
        public ListPageViewCS(ListPageViewModelBase listPageViewModel)
        {
            this.ListPageCollectionViewModel = listPageViewModel;
            AddContent();
            BindingContext = this.ListPageCollectionViewModel;
        }

        public ListPageViewModelBase ListPageCollectionViewModel { get; set; }
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
            LayoutHelpers.AddToolBarItems(this.ToolbarItems, this.ListPageCollectionViewModel.Buttons);
            Title = this.ListPageCollectionViewModel.FormSettings.Title;

            page = new Grid
            {
                Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ListPageViewLayoutStyle),
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                Children =
                {
                    new Label
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.HeaderStyle)
                    }
                    .AddBinding(Label.TextProperty, new Binding(nameof(ListPageViewModelBase.Title)))
                    .SetGridRow(0),
                    new CollectionView
                    {
                        Style = LayoutHelpers.GetStaticStyleResource(StyleKeys.ListFormCollectionViewStyle),
                        ItemTemplate = LayoutHelpers.GetCollectionViewItemTemplate
                        (
                            this.ListPageCollectionViewModel.FormSettings.ItemTemplateName,
                            this.ListPageCollectionViewModel.FormSettings.Bindings
                        )
                    }
                    .AddBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(ListPageViewModel<>.Items)))
                    .SetGridRow(1)
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