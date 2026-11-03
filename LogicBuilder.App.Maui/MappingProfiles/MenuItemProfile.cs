using AutoMapper;
using LogicBuilder.App.Maui.ViewModels;
using LogicBuilder.App.Maui.Forms.Configuration.Navigation;

namespace LogicBuilder.App.Maui.MappingProfiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<NavigationMenuItemDescriptor, FlyoutMenuItem>();
        }
    }
}
