using Microsoft.Maui.Controls;

namespace LogicBuilder.App.Maui.Views;

public partial class Flyout : ContentPage
{
    public CollectionView ListView { get; }

    public Flyout()
	{
		InitializeComponent();
        ListView = MenuItemsListView;
    }
}