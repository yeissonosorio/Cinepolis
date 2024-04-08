using System.Collections.ObjectModel;

namespace Cinepolis;

public partial class Cartelera : ContentPage
{
	string Ciudad;
    public ObservableCollection<models.Peliculas> Posts { get; set; }
    public Cartelera(string ciuda)
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        Ciudad = ciuda;
        Posts = new ObservableCollection<models.Peliculas>();
        LoadData();
        BindingContext = this;
    }

    public async void LoadData()
    {
        activityIndicator.IsRunning = true;

        var posts = await controllers.ControllerPelicula.Historial();
        if (posts != null)
        {
            foreach (var post in posts)
            {
                post.descripcion = SplitDescription(post.descripcion, 47);
                Posts.Add(post);
            }
        }
        else
        {

        }
        activityIndicator.IsVisible = false;
    }
    private string SplitDescription(string description, int maxLength)
    {
        var lines = Enumerable.Range(0, (int)Math.Ceiling((double)description.Length / maxLength))
            .Select(i => description.Substring(i * maxLength, Math.Min(description.Length - i * maxLength, maxLength)));
        return string.Join(Environment.NewLine, lines);
    }

    private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is models.Peliculas selectedReservation)
        {
            int id = selectedReservation.id;
            string nombre = selectedReservation.nombre;
            string imagen = selectedReservation.imagen;
            await Navigation.PushAsync(new Horario(id,nombre,imagen,Ciudad));
        }
    }
}