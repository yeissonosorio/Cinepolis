using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using Firebase.Database;
using Firebase.Database.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cinepolis
{
    public partial class Sala_Reserva : ContentPage
    {
        int count = 0;
        int a = 0;
        int Id_pelicula = 1;
        string Ciudad ;
        string Fecha ;
        string FechaI;
        string Hora ;
        string Imagen;
        string Nombre;
        string url;
        private List<string> nombresSeleccionados = new List<string>();
        private List<string> aciento = new List<string>();
        List<Clase.Producto> pro = new List<Clase.Producto>();
        public ObservableCollection<ElementoModel> Elementos { get; set; }
        private bool isLoaded = false;

        public ObservableCollection<models.acientos> Posts { get; set; }

        public Sala_Reserva(int id,string nombre,string imagen,string ciudad,string fechan,string fechab,string hora)
        {
            InitializeComponent();

            Shell.SetTabBarIsVisible(this, false);
            activityIndicator.IsRunning = true;
            activityIndicator.IsVisible = true;
            Ciudad = ciudad;
            Id_pelicula = id;
            Nombre = nombre;
            Imagen = imagen;
            Fecha= fechab;
            FechaI = fechan;
            Hora = hora;

            nom.Text = Nombre;
            ubi.Text = Ciudad;
            calendario.Text = FechaI+" "+Hora;

            if(Ciudad== "San Pedro Sula")
            {
                url = "San+Pedro+Sula";
            }
            else if(Ciudad== "Tegucigalpa") {
                url = "Tegucigalpa";
            }

            if (!string.IsNullOrEmpty(imagen))
            {
                poster.Source = ConvertirImagenBase64AImageSource(imagen);
            }

            Elementos = new ObservableCollection<ElementoModel>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!isLoaded) // Verificar si los elementos ya se han cargado antes
            {
                LoadDatas(); // Cargar datos
                isLoaded = true; // Establecer que los elementos ya se han cargado
                await VerificarYEliminarRegistrosAntiguos();
            }
        }

        private async void LoadData()
        {
            await DisplayAlert("", "Espere mientras cargan los asientos, presione OK", "OK");

            int a = 0;
            int totalSeats = 40; // Total de asientos a considerar

            for (int i = 0; i < totalSeats; i++)
            {
                string currentSeat = "";

                if (i >= 0 && i <= 7)
                {
                    currentSeat = "F" + (41 - (i + 1));
                }
                else if (i > 7 && i <= 15)
                {
                    currentSeat = "D" + (41 - (i + 1));
                }
                else if (i > 15 && i <= 23)
                {
                    currentSeat = "C" + (41 - (i + 1));
                }
                else if (i > 23 && i <= 31)
                {
                    currentSeat = "B" + (41 - (i + 1));
                }
                else if (i > 31 && i <= 39)
                {
                    currentSeat = "A" + (41 - (i + 1));
                }

                // Comprueba si hay un elemento en 'aciento' y si coincide con el asiento actual
                if (a < aciento.Count && aciento[a] == currentSeat)
                {
                    a = a + 1;
                    // Si coincide, agrega el asiento con el icono 'iconore.png'
                    Elementos.Add(new ElementoModel { Icono = "iconore.png", Name = currentSeat });
                    // Incrementa 'a' para comparar el siguiente elemento en 'aciento'
                    
                }
                else
                {
                    // Si no coincide, agrega el asiento con el icono 'iconoa.png'
                    Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = currentSeat });
                }
            }

            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
            indi.IsVisible = false;
        }





        private async void Image_Tapped(object sender, EventArgs e)
        {
            var image = (Image)sender; // Obtén la imagen que disparó el evento
            var elementoModel = (ElementoModel)image.BindingContext; // Obtén el elemento asociado a la imagen
            var nombre = elementoModel.Name; // Obtén el nombre del elemento
            if (elementoModel.Icono == "iconoa.png")
            {

                FirebaseClient firebaseClient = new FirebaseClient("https://cinepolis-119be-default-rtdb.firebaseio.com/");
                // Construye la clave del registro que deseas eliminar
                string claveBusqueda = $"{Id_pelicula}-{nombre}-{Ciudad}-{Fecha}-{Hora}";

                var registros = await firebaseClient
                .Child("Reserva")
                .OrderBy(nameof(models.reserva.clave))
                .EqualTo(claveBusqueda)
                .OnceAsync<models.reserva>();

                // Verifica si se encontró el registro
                if (registros.Count > 0)
                {
                    // La clave existe en la base de datos
                    await DisplayAlert("Aviso", "La silla esta en Proceso de reserva", "Ok");
                }
                else
                {
                    count += 60;
                    nombresSeleccionados.Add(nombre);
                    tot.Text = "L." + count;
                    LabelN.Text = string.Join(", ", nombresSeleccionados);
                    DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    long timestamp = (long)(DateTime.UtcNow - epochStart).TotalMilliseconds;
                    elementoModel.SetIcono("iconoac.png");
                    // La clave no existe en la base de datos
                    Console.WriteLine("La clave no existe en la base de datos.");
                    await firebaseClient.Child("Reserva").PostAsync(new models.reserva
                    {
                        clave = $"{Id_pelicula}-{nombre}-{Ciudad}-{Fecha}-{Hora}",
                        FechaHoraCreacionTimestamp = timestamp
                    }); ;

                }
            }
            else if (elementoModel.Icono == "iconore.png")
            {
                await DisplayAlert("Aviso", "Aciento Ocupado", "Ok");

            }
            else
            {
                elementoModel.SetIcono("iconoa.png");
                count -= 60;
                nombresSeleccionados.Remove(nombre);
                tot.Text = "L." + count;
               
                if (LabelN.Text =="")
                {
                    LabelN.Text = "No hay sillas seleccionadas";
                }
                FirebaseClient firebaseClient = new FirebaseClient("https://cinepolis-119be-default-rtdb.firebaseio.com/");
                // Construye la clave del registro que deseas eliminar
                string claveBusqueda = $"{Id_pelicula}-{nombre}-{Ciudad}-{Fecha}-{Hora}";

                var registros = await firebaseClient
                .Child("Reserva")
                .OrderBy(nameof(models.reserva.clave))
                .EqualTo(claveBusqueda)
                .OnceAsync<models.reserva>();

                if (registros.Any())
                {
                    var claveRegistro = registros.First().Key;
                    await firebaseClient.Child("Reserva").Child(claveRegistro).DeleteAsync();
                    LabelN.Text = string.Join(", ", nombresSeleccionados);
                }
                else
                {
                    // Manejar el caso en que no se encontró el registro
                }
            }

        }

        public async void LoadDatas(){
            var posts = await controllers.controlleraciento.GetPosts(Id_pelicula,url,Fecha,Hora);
            if (posts != null)
            {
                foreach (var post in posts)
                {
                    aciento.Add(post.aciento);
                }
                LoadData();

            }
            else
            {
                LoadData();
            }
        }


        public async void btncontinuar_click(object sender, EventArgs e)
        {
            if (nombresSeleccionados.Count==0)
            {
                await DisplayAlert("Aviso", "seleccione una silla", "OK");
            }
            else
            {
                bool result = await DisplayAlert("", "Desea agregar un snack a su reserva", "SI", "NO");
                if (result)
                {
                    await Navigation.PushAsync(new snack(nombresSeleccionados,Id_pelicula,Ciudad,Fecha,Hora,Imagen,Nombre,FechaI));
                }
                else
                {
                    await Navigation.PushAsync(new reserva_acept(pro,nombresSeleccionados,Id_pelicula,Ciudad,Fecha,Hora,Imagen,Nombre,FechaI));
                }
            }
        }

        public class ElementoModel : INotifyPropertyChanged
        {
            private string _icono;
            public string Icono
            {
                get { return _icono; }
                set
                {
                    if (_icono != value)
                    {
                        _icono = value;
                        OnPropertyChanged(nameof(Icono));
                    }
                }
            }

            public string Name { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            // Método para cambiar el icono que activa la notificación de cambio de propiedad
            public void SetIcono(string nuevoIcono)
            {
                Icono = nuevoIcono;
            }
        }

        private async Task VerificarYEliminarRegistrosAntiguos()
        {
            try
            {
                FirebaseClient firebaseClient = new FirebaseClient("https://cinepolis-119be-default-rtdb.firebaseio.com/");
                DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime horaLimite = DateTime.UtcNow - TimeSpan.FromMinutes(10); // Se establece la hora límite para eliminar registros antiguos
                long horaLimiteTimestamp = (long)(horaLimite - epochStart).TotalMilliseconds; // Utiliza la hora límite para calcular el timestamp

                var registrosAntiguos = await firebaseClient
                    .Child("Reserva")
                    .OrderBy(nameof(models.reserva.FechaHoraCreacionTimestamp))
                    .EndAt(horaLimiteTimestamp) // Filtra los registros con marca de tiempo menor o igual a horaLimiteTimestamp
                    .OnceAsync<models.reserva>();

                // Muestra los registros antiguos con marca de tiempo menor que horaLimiteTimestamp
                foreach (var registro in registrosAntiguos)
                {
                    await firebaseClient.Child("Reserva").Child(registro.Key).DeleteAsync();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                Console.WriteLine("Error al verificar y eliminar registros antiguos: " + ex.Message);
            }
        }

        private ImageSource ConvertirImagenBase64AImageSource(string imagenBase64)
        {
            if (string.IsNullOrEmpty(imagenBase64))
                return null;

            byte[] imageBytes = Convert.FromBase64String(imagenBase64);
            Stream imageStream = new MemoryStream(imageBytes);
            return ImageSource.FromStream(() => imageStream);
        }
    }
}
