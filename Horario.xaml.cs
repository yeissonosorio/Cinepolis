namespace Cinepolis;

public partial class Horario : ContentPage
{
	int Id;
	string Ciudad;
	string Nombre;
	string Imagen;
    string hora;
    string fechan;
    string fechab;
    DateTime fechaSeleccionada;
    public Horario(int id, string nombre, string imagen, string ciudad)
	{
		InitializeComponent();
        Shell.SetTabBarIsVisible(this, false);
        Id = id;
		Nombre= nombre;
		Imagen= imagen;
		Ciudad= ciudad;

		nom.Text = Nombre;
		ub.Text = Ciudad;

        if (!string.IsNullOrEmpty(imagen))
        {
            poster.Source = ConvertirImagenBase64AImageSource(imagen);
        }
        fechaSeleccionada = DateTime.Today;
        ConfigurarDatePicker();
    }

    private ImageSource ConvertirImagenBase64AImageSource(string imagenBase64)
    {
        if (string.IsNullOrEmpty(imagenBase64))
            return null;

        byte[] imageBytes = Convert.FromBase64String(imagenBase64);
        Stream imageStream = new MemoryStream(imageBytes);
        return ImageSource.FromStream(() => imageStream);
    }

    private void ConfigurarDatePicker()
    {
        // Obtener la fecha actual
        DateTime fechaActual = DateTime.Today;

        // Calcular la última fecha del mes
        DateTime ultimaFechaDelMes = new DateTime(fechaActual.Year, fechaActual.Month, DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month));

        // Establecer el rango del DatePicker
        datePicker.MinimumDate = fechaActual;
        datePicker.MaximumDate = ultimaFechaDelMes;
    }
    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        // Guardar la fecha seleccionada en la variable fechaSeleccionada
        fechaSeleccionada = e.NewDate;
    }

    private void OnButtonClicked4(object sender, EventArgs e)
    {
        DateTime fechaHoraSeleccionada = fechaSeleccionada.Date + TimeSpan.FromHours(16); // 4:00 PM
        hora = "4:00pm";
        ValidarFechaHora(fechaHoraSeleccionada);
    }
    private void OnButtonClicked6(object sender, EventArgs e)
    {
        DateTime fechaHoraSeleccionada = fechaSeleccionada.Date + TimeSpan.FromHours(18); // 6:00 PM
        hora = "6:00pm";
        ValidarFechaHora(fechaHoraSeleccionada);
    }
    private void OnButtonClicked8(object sender, EventArgs e)
    {
        DateTime fechaHoraSeleccionada = fechaSeleccionada.Date + TimeSpan.FromHours(20); // 8:00 PM
        hora = "8:00pm";
        ValidarFechaHora(fechaHoraSeleccionada);
    }

    private async void ValidarFechaHora(DateTime fechaHoraSeleccionada)
    {
        fechan = fechaSeleccionada.ToString("dd-MM-yyyy");
        fechab = fechaSeleccionada.ToString("yyyy-MM-dd");
        DateTime fechaHoraActual = DateTime.Now;

        // Comparar fecha y hora
        if (fechaHoraSeleccionada > fechaHoraActual)
        {
            await Navigation.PushAsync(new Sala_Reserva(Id, Nombre, Imagen, Ciudad, fechan, fechab, hora));
        }
        else
        {
            // La fecha y hora seleccionada es menor o igual que la fecha y hora actual
            await DisplayAlert("Error", "La fecha y hora seleccionada debe ser mayor que la fecha y hora actual", "OK");
        }
    }
}