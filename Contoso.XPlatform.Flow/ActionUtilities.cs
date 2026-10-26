using LogicBuilder.App.Maui.Forms.Parameters.Navigation;

namespace Contoso.XPlatform.Flow
{
    public static class ActionUtilities
    {
        public static void UpdateNavigationBar(IActions actions, NavigationBarParameters navBar)
        {
            actions.UpdateNavigationBar(navBar);
        }
    }
}
