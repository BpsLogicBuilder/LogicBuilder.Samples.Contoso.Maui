using LogicBuilder.App.Maui.ViewModels.TextPage;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public class TextPageFactory : ITextPageFactory
    {
        private readonly Func<TextPageViewModel, TextPageViewCS> _getPage;
        private readonly Func<ScreenSettingsBase, TextPageViewModel> _getViewModel;

        public TextPageFactory(
            Func<TextPageViewModel, TextPageViewCS> getPage,
            Func<ScreenSettingsBase, TextPageViewModel> getViewModel)
        {
            _getPage = getPage;
            _getViewModel = getViewModel;
        }

        public Page CreatePage(ScreenSettingsBase screenSettings) 
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}
