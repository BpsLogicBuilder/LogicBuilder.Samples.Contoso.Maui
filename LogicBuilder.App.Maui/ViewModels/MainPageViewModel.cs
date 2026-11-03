using System.Collections.ObjectModel;

namespace LogicBuilder.App.Maui.ViewModels
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
