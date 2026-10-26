using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Contoso.XPlatform.Views;

public partial class BusyIndicator : ContentPage
{
	public BusyIndicator()
	{
		InitializeComponent();
        this.BackgroundColor = Colors.Transparent;
    }
}