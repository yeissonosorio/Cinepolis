namespace Cinepolis;

public partial class password : ContentPage
{
	string op;
	public password(string Op)
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
    }
    public async void btnlogin_Clicked(object sender, EventArgs e)
    {
        if (ver()== true)
        {
            await Navigation.PushAsync(new login(op));
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