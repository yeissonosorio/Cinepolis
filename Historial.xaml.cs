namespace Cinepolis;

public partial class Historial : ContentPage
{
    string his = "";
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
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new Secion("Historial"));
    }
}