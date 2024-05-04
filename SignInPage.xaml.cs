using ISIEvents.Pages;
namespace ISIEvents
{

    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
        }


        private async void SignedIn(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(mail.Text)&& !string.IsNullOrEmpty(pwd.Text))
            {
                if (mail.Text.ToLower().Equals("admin@isi.utm.tn"))
                {
                    if (pwd.Text.Equals("ISIadmin2024"))
                    {
                        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                    }
                }
                else
                {
                    await DisplayAlert("Problem", "Check credentials", "ok");
                }
            }
            else
            {
                await DisplayAlert("Problem", "Empty ! ", "ok");
            }


        }
    }
}