using Firebase.Database;
using ISIEvents.Entities;
using System.Collections.ObjectModel;

namespace ISIEvents.Pages;

public partial class StudentList : ContentPage
{
    public FirebaseClient FirebaseClient { get; set; }
    ObservableCollection<Student> L;

    public StudentList()
	{
		InitializeComponent();
        FirebaseClient = new FirebaseClient("https://projetmobile-e829e-default-rtdb.europe-west1.firebasedatabase.app/");
    }
    async void Loadpeople()
    {
        try
        {
            var I = await FirebaseClient.Child("Students").OnceAsync<Student>();

            L.Clear();

            foreach (var image in I)
            {
                L.Add(new Student
                {
                    Id = image.Object.Id,
                    Name = image.Object.Name,
                    Email = image.Object.Email,
                    PhoneNumber = image.Object.PhoneNumber,
                    Password = image.Object.Password,
                    Categories = image.Object.Categories
                });
            }

            userListView.ItemsSource = L;


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading images: {ex.Message}");
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Loadpeople();
    }

}