using Firebase.Auth;
using Firebase.Auth.Providers;
using System.Reflection;

namespace Cinepolis;

public partial class singup : ContentPage
{
    
    public singup()
    {
        InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        
       
    }
    public async void btnlogin_Clicked(object sender, EventArgs e)
    {
        if (ver() == true)
        {
            try
            {
                activityIndicator.IsVisible = true;
                firebase con = new firebase();
               string email = UsernameEntry.Text;
               string pass = PasswordEntry.Text;
                var userCredential = await con.CrearUsuario(email, pass);

                var usuer = new models.usuario_cliente
                {
                    correo = email,
                    estado = 0
                };

                if (userCredential != null)
                {
                    models.Msg msg = await controllers.controllercliente.CreateEmple(usuer);

                    if (msg != null)
                    {
                        await DisplayAlert("Success", "El usuario se registró correctamente. Se ha enviado un correo de verificación.", "OK");
                    }
                    
                }
                else
                {
                    await DisplayAlert("Error", "Este correo ya existe", "OK");
                }
                activityIndicator.IsVisible = false;
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Este correo ya existe", "OK");
                activityIndicator.IsVisible = false;
            }
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

                if(CorreoEntry.Text.Length >= 6) {
                    return true;
                }
                else
                {
                    DisplayAlert("Aviso", "Contraseña debe ser mayor o igual a 6 caracteres ", "ok");
                    return false;
                }
            }
            else
            {
                DisplayAlert("Aviso", "Contraseña no coinciden", "ok");
                return false;
            }
        }
    }
    
}