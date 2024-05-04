using CommunityToolkit.Maui.Views;
using Firebase.Database;
using ISIEvents.Entities;
using System.Collections.ObjectModel;

namespace ISIEvents.Pages
{
    public partial class MainPage : ContentPage
    {
        FirebaseClient firebaseClient;
        ObservableCollection<Event> imagesList;
        public MainPage()
        {
            InitializeComponent();

            firebaseClient = new FirebaseClient("https://projetmobile-e829e-default-rtdb.europe-west1.firebasedatabase.app/");
            imagesList = new ObservableCollection<Event>();
            LoadImages();
        }


        async void LoadImages()
        {
            try
            {
                var images = await firebaseClient.Child("Events").OnceAsync<Event>();

                imagesList.Clear();

                foreach (var image in images)
                {
                    imagesList.Add(new Event
                    {
                        Id = image.Object.Id,
                        Name = image.Object.Name,
                        Categorie = image.Object.Categorie,
                        Description = image.Object.Description,
                        EventURL = image.Object.EventURL
                    });
                }

                listView.ItemsSource = imagesList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading images: {ex.Message}");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadImages();
        }

        private void event_happened(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (e.SelectedItem != null && e.SelectedItem is Event selectedEvent)
            {
                this.ShowPopupAsync(new Page(selectedEvent));
                listView.SelectedItem = null;
            }

        }
    }
}
