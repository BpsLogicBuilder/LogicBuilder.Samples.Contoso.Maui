using LogicBuilder.App.Maui.ViewModels.SearchPage;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public class SearchPageFactory(
        Func<SearchPageViewModelBase, SearchPageViewCS> getPage,
        Func<ScreenSettingsBase, SearchPageViewModelBase> getViewModel) : ISearchPageFactory
    {
        private readonly Func<SearchPageViewModelBase, SearchPageViewCS> _getPage = getPage;
        private readonly Func<ScreenSettingsBase, SearchPageViewModelBase> _getViewModel = getViewModel;

        public Page CreatePage(ScreenSettingsBase screenSettings)
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}
