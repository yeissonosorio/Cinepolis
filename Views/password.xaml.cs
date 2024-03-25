namespace Cinepolis;

public partial class password : ContentPage
{
	
	public password()
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
    }
    public async void btnlogin_Clicked(object sender, EventArgs e)
    {
        if (ver()== true)
        {
            firebase con = new firebase();
            string email = UsernameEntry.Text;
            con.enviar(email);
            await DisplayAlert("Recuperar", "Se envio codigo de recuperacion", "Ok");
            await Navigation.PushAsync(new login());
        }  
    }
    public Boolean ver()
    {
   
        if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
        {
            DisplayAlert("Aviso", "Ingrese el Email", "ok");
            return false;
        }
        else
        {
            return true;
        }
    }
}