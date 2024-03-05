namespace Cinepolis
{
    public partial class MainPage : ContentPage
    {
        private bool frameEnabled = true;
        private bool frameEnabled1 = true;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        //Evento al precionar el icono de inicio Secion
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new login("cine"));
        }

        //Evento al precionar el frame de San Pedro Sula
        private async void SPS(object sender, EventArgs e)
        {
            if (frameEnabled)
            {
                // Desactivar el Frame para evitar clics múltiples
                frameEnabled = false;

                // Aquí puedes implementar la navegación a otra página
                await Navigation.PushAsync(new informacion_reserva());


                // Habilitar el Frame después de un retraso
                await Task.Delay(1000); // Cambia este valor 
                frameEnabled = true;
            }
        }

        //Evento al precionar el frame de tegucigalpa
        private async void TGU(object sender, EventArgs e)
        {
            if (frameEnabled1)
            {
                // Desactivar el Frame para evitar clics múltiples
                frameEnabled1 = false;

                // Aquí puedes implementar la navegación a otra página
                await Navigation.PushAsync(new Sala_Reserva());

                // Habilitar el Frame después de un retraso
                await Task.Delay(1000); // Cambia este valor 
                frameEnabled1 = true;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            // Aquí no hacemos nada, evitando que el botón de retroceso tenga efecto
            return true; // Indica que hemos manejado el evento nosotros mismos
        }

    }
}
