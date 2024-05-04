using CommunityToolkit.Maui.Views;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using ISIEvents.Entities;

namespace ISIEvents.Pages;

public partial class PopupAdd : Popup
{
    public FirebaseClient FirebaseClient { get; set; }

    public PopupAdd()
	{
		InitializeComponent();

        FirebaseClient = new FirebaseClient("https://projetmobile-e829e-default-rtdb.europe-west1.firebasedatabase.app/");
        BindingContext = this;
    }

    private async void SaveCat_Clicked(object sender, EventArgs e)
    {
        if(category_name.Text.Length > 0)
        {
            var entry = new Category
            {
                Name = category_name.Text,
                Description = description.Text
            };

            await FirebaseClient
                .Child("Categorie")
                .PostAsync(entry);
        }
      
    }
}