using LogicBuilder.App.Maui.ViewModels.ListPage;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public class ListPageFactory(
        Func<ListPageViewModelBase, ListPageViewCS> getPage,
        Func<ScreenSettingsBase, ListPageViewModelBase> getViewModel) : IListPageFactory
    {
        private readonly Func<ListPageViewModelBase, ListPageViewCS> _getPage = getPage;
        private readonly Func<ScreenSettingsBase, ListPageViewModelBase> _getViewModel = getViewModel;

        public Page CreatePage(ScreenSettingsBase screenSettings)
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}
