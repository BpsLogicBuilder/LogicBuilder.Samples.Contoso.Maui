using System.Collections.ObjectModel;

namespace Contoso.XPlatform.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<FlyoutMenuItem> _menuItems = [];
        public ObservableCollection<FlyoutMenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (_menuItems == value)
                    return;

                _menuItems = value;
                OnPropertyChanged();
            }
        }
    }
}
