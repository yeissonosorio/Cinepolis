using System.Collections.ObjectModel;
namespace Cinepolis;

public partial class login : ContentPage
{
    string Op;
    public ObservableCollection<models.usuario_cliente> Posts { get; set; }
    public login(string op)
    {
        InitializeComponent();
        Posts = new ObservableCollection<models.usuario_cliente>();
       
        Shell.SetTabBarIsVisible(this, false);
        Op = op;
        
    }

    public async void btnsingup_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new singup(Op));

    }
    public async void btnlogin_Clicked(object sender, EventArgs e)
    {
        if (ver() == true)
        {
            try
            {
                firebase con = new firebase();
                string email = UsernameEntry.Text;
                string pass = PasswordEntry.Text;
                var userCredential = await con.CargarUsuario(email, pass);

                if (userCredential != null)
                {
                   await LoadData(email);       
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error", "Correo o contraseña no valido", "ok");
            }
        }
    }
    private async void Label_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new password(Op));
    }

    public Boolean ver()
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) && string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            DisplayAlert("Aviso", "Llene todos los campos", "ok");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
        {
            DisplayAlert("Aviso", "Ingrese el Email", "ok");
            return false;
        }
        else if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            DisplayAlert("Aviso", "Ingrese la Contraseña", "ok");
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task LoadData(string correo)
    {
        try
        {
            var posts = await controllers.controllercliente.GetPosts(correo);
            if (posts != null)
            {
                // Limpiar la colección actual de Posts
                Posts.Clear();

                await DisplayAlert("Bienvenido", "" + posts.correo, "Ok");

                // Redirigir a la siguiente página según el valor de Op
                if (Op == "cine")
                {
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await Navigation.PushAsync(new Historial());
                }
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

    protected override bool OnBackButtonPressed()
    {
        // Ir a la página "OtraPagina"
        App.Current.MainPage.Navigation.PushAsync(new MainPage());

        return true; // Indica que hemos manejado el evento nosotros mismos
    }

}