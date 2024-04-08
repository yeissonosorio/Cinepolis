using System.Collections.ObjectModel;

namespace Cinepolis;

public partial class Historial : ContentPage
{
    public ObservableCollection<models.historialReservacion> Posts { get; set; }
    public Historial()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        Posts = new ObservableCollection<models.historialReservacion>();
        BindingContext = this;

    }
    protected override bool OnBackButtonPressed()
    {
        // Aquí no hacemos nada, evitando que el botón de retroceso tenga efecto
        return true; // Indica que hemos manejado el evento nosotros mismos
    }
    public void recarga()
    {
        string correo = Preferences.Get("correo", "");
        
        if (correo != "")
        {
            imagenpa.IsVisible = false;
            textosin.IsVisible = false;
            S.IsVisible = true; 
            LoadData();
            
        }
        else
        {
            imagenpa.IsVisible = true;
            textosin.IsVisible = true;
            activityIndicator.IsVisible = false;
        }
        
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Posts.Clear();
        recarga();
    }
    public async void LoadData()
    {
        int id = Preferences.Get("Id", 0);
        var posts = await controllers.historialControllers.Historial(id);
            if (posts != null)
            {
                foreach (var post in posts)
                {
                    post.total = $"L.{post.total}";
                    Posts.Add(post);
                }
                activityIndicator.IsVisible=false;
            }
            else
            {
                imagenpa.IsVisible = true;
                textosin.IsVisible = true;
                activityIndicator.IsVisible = false;
        }
    }

    private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is models.historialReservacion selectedReservation)
        {
            
            string fecha = selectedReservation.fecha+" "+selectedReservation.hora;
            await Navigation.PushAsync(new informacion_reserva(selectedReservation.imagen_pelicula,selectedReservation.nombre_pelicula,fecha,selectedReservation.nombre_sala,
                selectedReservation.id,selectedReservation.total));
        }
    }

}
