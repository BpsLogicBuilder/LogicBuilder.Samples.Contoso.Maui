using LogicBuilder.App.Maui.ViewModels.DetailForm;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public class DetailFormFactory(
        Func<DetailFormViewModelBase, DetailFormViewCS> getPage,
        Func<ScreenSettingsBase, DetailFormViewModelBase> getViewModel) : IDetailFormFactory
    {
        private readonly Func<DetailFormViewModelBase, DetailFormViewCS> _getPage = getPage;
        private readonly Func<ScreenSettingsBase, DetailFormViewModelBase> _getViewModel = getViewModel;

        public Page CreatePage(ScreenSettingsBase screenSettings)
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}