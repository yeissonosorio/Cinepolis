using System.Collections.ObjectModel;

namespace Cinepolis;

public partial class Historial : ContentPage
{
    ObservableCollection<string> Posts { get; set; } = new ObservableCollection<string>();
    string his = "ds";
	public Historial()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        if (his == "")
        {
            S.IsVisible = false;
        }
        else
        {
            imagenpa.IsVisible = false;
            textosin.IsVisible = false;
        }
        
        Posts.Add(his);

        postListView.ItemsSource = Posts;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new login("Historial"));
    }
    protected override bool OnBackButtonPressed()
    {
        // Aquí no hacemos nada, evitando que el botón de retroceso tenga efecto
        return true; // Indica que hemos manejado el evento nosotros mismos
    }
}