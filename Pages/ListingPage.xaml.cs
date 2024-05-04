using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using ISIEvents.Entities;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace ISIEvents.Pages{

public partial class ListingPage : ContentPage
{
        public ObservableCollection<string> Cat { get; set; }

        FirebaseClient firebaseDatabaseClient;
        FirebaseStorage firebaseStorage;
        FileResult selectedPhoto;

        public ListingPage()
        {
            InitializeComponent();
            Cat = new ObservableCollection<string>();

            BindingContext = this;
            firebaseDatabaseClient = new FirebaseClient("https://projetmobile-e829e-default-rtdb.europe-west1.firebasedatabase.app/");
            firebaseStorage = new FirebaseStorage("projetmobile-e829e.appspot.com");
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await pickeritems();

        }

        async void OnSelectPhotoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                selectedPhoto = await MediaPicker.PickPhotoAsync();

                if (selectedPhoto != null)
                {
                    SelectedPhotoImage.Source = ImageSource.FromFile(selectedPhoto.FullPath);
                    SelectedPhotoImage.IsVisible = true;
                    UploadPhotoButton.IsEnabled = true; // Enable the upload button
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        async void OnUploadPhotoButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var currentValue = await firebaseDatabaseClient
                    .Child("Event_counter")
                    .OnceSingleAsync<int>();

                int newValue = currentValue + 1;

                using (var stream = await selectedPhoto.OpenReadAsync())
                {
                    byte[] imageData = new byte[stream.Length];
                    await stream.ReadAsync(imageData, 0, (int)stream.Length);

                    string fileName = $"{Guid.NewGuid()}.jpg";

                    var photoReference = await firebaseStorage
                        .Child("Events")
                        .Child(currentValue.ToString())
                        .PutAsync(new System.IO.MemoryStream(imageData));

                    string imageUrl = photoReference;

                    var entry = new Event
                    {
                        Id = currentValue,
                        Name = event_name.Text,
                        Categorie = picker.SelectedItem.ToString(),
                        Description = description.Text,
                        EventURL = imageUrl
                    };

                    await firebaseDatabaseClient
                        .Child("Events")
                        .PostAsync(entry);

                    await firebaseDatabaseClient
                        .Child("Event_counter")
                        .PutAsync(newValue);
                }

                await DisplayAlert("Success", "Image uploaded successfully!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        async Task pickeritems()
        {
            Cat.Clear(); 

            var categ = await firebaseDatabaseClient.Child("Categorie").OnceAsync<Category>();

            foreach (var c in categ)
            {
                Cat.Add(c.Object.Name);
            }

            picker.ItemsSource = Cat;
            OnPropertyChanged(nameof(Cat));
        }
    }

}