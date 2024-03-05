namespace Cinepolis;

public partial class singup : ContentPage
{
    string op;
    public singup(string Op)
    {
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        
        op = Op;
    }
    public async void btnlogin_Clicked(object sender, EventArgs e)
    {
        if (ver() == true)
        {
            await Navigation.PushAsync(new login(op));
        }
    }

    public Boolean ver()
    {
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text) && string.IsNullOrWhiteSpace(PasswordEntry.Text) && string.IsNullOrWhiteSpace(CorreoEntry.Text))
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
        else if (string.IsNullOrWhiteSpace(CorreoEntry.Text))
        {
            return false;
        }
        else
        {
            if (CorreoEntry.Text == PasswordEntry.Text)
            {

                return true;
            }
            else
            {
                DisplayAlert("Aviso", "Contraseña no coinciden", "ok");
                return false;
            }
        }
    }
    
}