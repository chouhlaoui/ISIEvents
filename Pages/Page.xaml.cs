using CommunityToolkit.Maui.Views;
using ISIEvents.Entities;

namespace ISIEvents.Pages;

public partial class Page : Popup
{
	public Page(Event E)
	{
		InitializeComponent();
        BindingContext = E;
    }
}