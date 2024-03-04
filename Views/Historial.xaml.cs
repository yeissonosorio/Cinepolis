using System.Collections.ObjectModel;

namespace Cinepolis;

public partial class Historial : ContentPage
{
    ObservableCollection<string> Posts { get; set; } = new ObservableCollection<string>();
    string his = "ds";
	public Historial()
	{
		InitializeComponent();
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
        //await Navigation.PushAsync(new Secion("Historial"));
    }
}