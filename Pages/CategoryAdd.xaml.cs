using Firebase.Database;
using System.Collections.ObjectModel;
using ISIEvents.Entities;
using CommunityToolkit.Maui.Views;
namespace ISIEvents.Pages;

public partial class CategoryAdd : ContentPage
{
	public CategoryAdd()
	{
		InitializeComponent();
        FirebaseClient = new FirebaseClient("https://projetmobile-e829e-default-rtdb.europe-west1.firebasedatabase.app/");
        BindingContext = this;

    }
    public ObservableCollection<Category> Cat { get; set; } = new ObservableCollection<Category>();
    public FirebaseClient FirebaseClient { get; set; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DownloadAllCat();
    }
    public async void DownloadAllCat()
    {
        Cat.Clear();
        var categ = await FirebaseClient.Child("Categorie").OnceAsync<Category>();

        foreach (var c in categ)
        {
            Cat.Add(new Category
            {
                Name = c.Object.Name,
                Description = c.Object.Description
            });
        }
        listcat.ItemsSource = Cat;
    }
    private async void Save(object sender, EventArgs e)
    {
        await this.ShowPopupAsync(new PopupAdd());
    }
}