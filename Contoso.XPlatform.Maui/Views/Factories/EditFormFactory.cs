using Contoso.XPlatform.Flow.Settings.Screen;
using Contoso.XPlatform.ViewModels.EditForm;
using Microsoft.Maui.Controls;
using System;

namespace Contoso.XPlatform.Views.Factories
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