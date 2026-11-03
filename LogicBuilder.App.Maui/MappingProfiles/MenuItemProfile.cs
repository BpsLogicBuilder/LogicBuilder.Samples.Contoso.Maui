using AutoMapper;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;

namespace Contoso.XPlatform.MappingProfiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
