using LogicBuilder.App.Maui.ViewModels.EditForm;
using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;
using System;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public class EditFormFactory(
        Func<EditFormViewModelBase, EditFormViewCS> getPage,
        Func<ScreenSettingsBase, EditFormViewModelBase> getViewModel) : IEditFormFactory
    {
        private readonly Func<EditFormViewModelBase, EditFormViewCS> _getPage = getPage;
        private readonly Func<ScreenSettingsBase, EditFormViewModelBase> _getViewModel = getViewModel;

        public Page CreatePage(ScreenSettingsBase screenSettings)
            => _getPage
            (
                _getViewModel(screenSettings)
            );
    }
}