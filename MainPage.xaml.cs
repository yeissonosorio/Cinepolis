
using Plugin.LocalNotification;
using System.Reactive;


namespace Cinepolis
{
    public partial class MainPage : ContentPage
    {
        private bool frameEnabled = true;
        int cont = 1;
        private bool frameEnabled1 = true;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            
            if(Preferences.Get("correo", "") == "")
            {
                Log.IconImageSource = ImageSource.FromFile("iconos.png");
            }
            else
            {
                Log.IconImageSource = ImageSource.FromFile("salir.png");
            }
        }

        

        //Evento al precionar el icono de inicio Secion
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (Preferences.Get("correo", "") == "")
            {
                await Navigation.PushAsync(new login());
            }
            else
            {
                Preferences.Set("Id", 0);
                Preferences.Set("correo", "");
                await DisplayAlert("", "Cerrando sesión", "Ok");
                Log.IconImageSource = ImageSource.FromFile("iconos.png");
                await Navigation.PopToRootAsync();
            }
            
        }

        //Evento al precionar el frame de San Pedro Sula
        private async void SPS(object sender, EventArgs e)
        {
            string correo = Preferences.Get("correo", "");
            if (correo != "")
            {
                if (frameEnabled)
                {
                    // Desactivar el Frame para evitar clics múltiples
                    frameEnabled = false;

                    // Aquí puedes implementar la navegación a otra página
                    await Navigation.PushAsync(new Cartelera("San Pedro Sula"));
                    // Habilitar el Frame después de un retraso
                    await Task.Delay(1000); // Cambia este valor 
                    frameEnabled = true;
                }
            }
            else
            {
                await DisplayAlert("","Debe iniciar sesión","Ok");
            }
        }

        //Evento al precionar el frame de tegucigalpa
        private async void TGU(object sender, EventArgs e)
        {
            string correo = Preferences.Get("correo", "");
            if (correo != "")
            {
                if (frameEnabled1)
                {
                    // Desactivar el Frame para evitar clics múltiples
                    frameEnabled1 = false;

                    // Aquí puedes implementar la navegación a otra página
                    await Navigation.PushAsync(new Cartelera("Tegucigalpa"));

                    // Habilitar el Frame después de un retraso
                    await Task.Delay(1000); // Cambia este valor 
                    frameEnabled1 = true;
                }
            }
            else
            {
                await DisplayAlert("", "Debe iniciar sesión", "Ok");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Aquí no hacemos nada, evitando que el botón de retroceso tenga efecto
            return true; // Indica que hemos manejado el evento nosotros mismos
        }

    }
}
