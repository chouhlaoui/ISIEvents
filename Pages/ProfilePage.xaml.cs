namespace ISIEvents.Pages
{

public partial class ProfilePage : ContentPage
{

    public ProfilePage()
	{
		InitializeComponent();
    }

    private void LogOut(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(SignInPage)}");
    }
}

}