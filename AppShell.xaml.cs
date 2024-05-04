using ISIEvents.Pages;


namespace ISIEvents
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ListingPage), typeof(ListingPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(StudentList), typeof(StudentList));
            Routing.RegisterRoute(nameof(CategoryAdd), typeof(CategoryAdd));
        }
    }
}
