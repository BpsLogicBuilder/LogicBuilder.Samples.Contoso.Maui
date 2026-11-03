using Microsoft.Maui.Controls;

namespace Contoso.XPlatform.Views;

public partial class Flyout : ContentPage
{
    public CollectionView ListView { get; }

    public Flyout()
	{
		InitializeComponent();
        ListView = MenuItemsListView;
    }
}