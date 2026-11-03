using LogicBuilder.App.Maui.Business.Settings.Screen;
using Microsoft.Maui.Controls;

namespace LogicBuilder.App.Maui.Views.Factories
{
    public interface IFlyoutDetailPageFactory
    {
        Page CreatePage(ScreenSettingsBase screenSettings);
    }
}
