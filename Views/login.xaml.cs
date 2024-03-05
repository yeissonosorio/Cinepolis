namespace Cinepolis;

public partial class login : ContentPage
{
    string Op;
    public login(string op)
    {
        InitializeComponent();
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
            if (Op == "cine")
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Navigation.PushAsync(new Historial());
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
    protected override bool OnBackButtonPressed()
    {
        // Ir a la página "OtraPagina"
        App.Current.MainPage.Navigation.PushAsync(new MainPage());

        return true; // Indica que hemos manejado el evento nosotros mismos
    }
}