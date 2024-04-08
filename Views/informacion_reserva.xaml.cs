using Cinepolis.models;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Cinepolis;

public partial class informacion_reserva : ContentPage
{
    public ObservableCollection<models.acent> Posts { get; set; }
    public ObservableCollection<models.versanck> Postss { get; set; }
    string tot;
    public informacion_reserva(string imagen,string nombre,string Fecha,int Sala,int id,string total)
	{
		InitializeComponent();
        Posts = new ObservableCollection<models.acent>();
        Postss = new ObservableCollection<models.versanck>();
        tot = total;
        LoadData(id);
        LoadDatasnack(id);
		nom.Text = nombre;
		fecha.Text = Fecha;
		sal.Text = "Sala: #" + Sala;
        if (!string.IsNullOrEmpty(imagen))
        {
            poster.Source = ConvertirImagenBase64AImageSource(imagen);
        }
    }

    public async Task LoadData(int id)
    {
        try
        {
            var posts = await controllers.controldetalle.GetPosts(id);

            if (posts != null)
            {
                ub.Text = posts.ciudad;
                canti.Text = "Cantidad de Entradas: " + posts.count;
                totbol.Text = "Total de entradas: L."+(posts.count*60);
            }
            else
            {
                // Manejar caso de posts nulos
                await DisplayAlert("Error", "No se encontraron datos de usuario", "OK");
            }
        }
        catch (Exception ex)
        {
            // Manejar otras excepciones
            await DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
        }
    }

    public async void LoadDatasnack(int id)
    {
        var pots = await controllers.controldetalle.Historialsnack(id);
        if (pots != null)
        {
             
            Label nuevoLabel = new Label
            {
                Text = "Snack inclidos",
                FontSize = 20,
                Margin = new Thickness(0, 0, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,

            };

            conte.Children.Add(nuevoLabel);

            foreach (var post in pots)
            {
                Label snack = new Label
                {
                    Text = post.cantiad+" "+post.combo+" Precio: L." +post.precio+" total: L."+post.total,
                    FontSize = 20,
                    Margin = new Thickness(0, 0, 0, 0),
                    FontFamily = "OswaldRegular",
                    HorizontalTextAlignment = TextAlignment.Center,
                   
                };
                conte.Children.Add(snack);
            }
            Label tota = new Label
            {
                Text = "Total de reservación: " + tot,
                FontSize = 20,
                Margin = new Thickness(0, 0, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };
            conte.Children.Add(tota);
        }
        else
        {
            Label nuevoLabel = new Label
            {
                Text = "Total de reservación: "+tot,
                FontSize = 20,
                Margin = new Thickness(0, 0, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };
            conte.Children.Add(nuevoLabel);
        }
    }
    private ImageSource ConvertirImagenBase64AImageSource(string imagenBase64)
    {
        if (string.IsNullOrEmpty(imagenBase64))
            return null;

        byte[] imageBytes = Convert.FromBase64String(imagenBase64);
        Stream imageStream = new MemoryStream(imageBytes);
        return ImageSource.FromStream(() => imageStream);
    }
}