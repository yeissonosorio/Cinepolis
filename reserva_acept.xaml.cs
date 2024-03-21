

using System.Collections.ObjectModel;
using QRCoder;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using System.Net;
using Cinepolis.models;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Cinepolis.Views;
namespace Cinepolis;

public partial class reserva_acept : ContentPage
{
	List<Clase.Producto> productos;
    List<string> acientos;
    public ObservableCollection<models.idreserva> Posts { get; set; }
    int totaacient=0;
    int totsnack = 0;
    int total = 0;
    int idre = 0;
    bool butom=false;
    public string datoRecibido { get; private set; }
    public string totalString { get; private set; }
    public reserva_acept(List<Clase.Producto> snack,List<string>acient)
	{
		InitializeComponent();
		this.productos = snack;
        this.acientos = acient;
        Posts = new ObservableCollection<models.idreserva>();
        Shell.SetTabBarIsVisible(this, false);
        acien();
        carga();
        cagarbarra();
        
    }
    public reserva_acept()
    {
        this.InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (Navigation.ModalStack.LastOrDefault() is PaymenState paginaPago)
        {
            datoRecibido = paginaPago.Status;
            if (datoRecibido == "COMPLETED")
            {
                await DisplayAlert("Compra exitosa!", "La compra fue exitosa. Gracias por su compra.", "OK");
                await Navigation.PushAsync(new MainPage());
            }
        }
    }


    public void carga()
	{
        if (productos.Count > 0)
        {
            Frame nuevoframe = new Frame
            {
                CornerRadius = 10,
                HasShadow = true, 
                ZIndex = 1, 
                Margin = 10
            };
            StackLayout nuevostack = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            Label nuevoLabel = new Label
            {
                Text = "Snack",
                FontSize = 20,
                Margin = new Thickness(0,0, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,

            };

            nuevostack.Children.Add(nuevoLabel);
            for (int i = 0; i < productos.Count; i++)
            {
                Label labpro = new Label
                {
                    Text = productos[i].cantidad + " " + productos[i].name + " precio: L." + productos[i].precio+" total: l." + productos[i].sub,
                    FontSize = 15,
                    Margin = new Thickness(10, 10, 0, 0),
                    FontFamily = "OswaldRegular",
                    HorizontalTextAlignment = TextAlignment.Start,
                };
                totsnack += productos[i].sub;
                nuevostack.Children.Add(labpro);
            }
            nuevoframe.Content = nuevostack;
            con.Children.Add(nuevoframe);
        }
    }

    public void acien()
    {
            Frame nuevoframe = new Frame
            {
                CornerRadius = 10,
                HasShadow = true,
                ZIndex = 1,
                Margin = 10
            };
            StackLayout nuevostack = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            Label nuevoLabel = new Label
            {
                Text = "Acientos",
                FontSize = 20,
                Margin = new Thickness(0, 0, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,

            };

            nuevostack.Children.Add(nuevoLabel);
            Label cantidad = new Label
            {
                Text = "Acientos"+acientos.Count,
                FontSize = 15,
                Margin = new Thickness(10, 10, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Start,
            };

            string nom = string.Join(",", acientos);
            Label Nombresacientos = new Label
            {
                Text = "Acientos: "+nom,
                FontSize = 15,
                Margin = new Thickness(10, 10, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Start,
            };
            totaacient = acientos.Count * 60;
            Label tot = new Label
            {
                Text = "Total: L." +totaacient,
                FontSize = 15,
                Margin = new Thickness(10, 10, 0, 0),
                FontFamily = "OswaldRegular",
                HorizontalTextAlignment = TextAlignment.Start,
            };

            nuevostack.Children.Add(cantidad);
            nuevostack.Children.Add(Nombresacientos);
            nuevostack.Children.Add(tot);
            nuevoframe.Content = nuevostack;
            con.Children.Add(nuevoframe);   
    }

    public async void cagarbarra()
    {
        StackLayout mainStackLayout = new StackLayout
        {
            VerticalOptions = LayoutOptions.FillAndExpand // Para que ocupe todo el espacio vertical disponible
        };

        Grid grid = new Grid();

        // Definir las RowDefinitions
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        // Crear el StackLayout en la segunda fila del Grid
        StackLayout stackLayout = new StackLayout
        {
            BackgroundColor = Color.FromHex("#0583fe"),
            HeightRequest = 50,
            Orientation = StackOrientation.Horizontal,
            Margin = new Thickness(0, 10, 0, 0)
        };

        // Crear los elementos dentro del StackLayout
        Image image1 = new Image();
        Image image2 = new Image { Source = "iconocarro.png" };
        total = totaacient + totsnack;
        Label label = new Label
        {
            Text = "L." + total,
            TextColor = Color.FromHex("#ffffff"),
            FontSize = 20,
            Padding = new Thickness(10)
        };
        Button button = new Button
        {
            Text = "Pagar",
            WidthRequest = 100,
            HeightRequest = 40,
            FontSize = 15,
            BackgroundColor = Color.FromHex("#ffffff"),
            TextColor = Color.FromHex("#0583fe"),
            FontFamily = "OswaldRegular",
            Padding = new Thickness(5, 0, 0, 5),
            HorizontalOptions = LayoutOptions.EndAndExpand,
            Margin = new Thickness(0, 0, 10, 0)
        };
        button.Clicked += btncontinuar_click;

        // Agregar los elementos al StackLayout
        stackLayout.Children.Add(image1);
        stackLayout.Children.Add(image2);
        stackLayout.Children.Add(label);
        stackLayout.Children.Add(button);

        // Agregar el StackLayout al Grid
        grid.Children.Add(stackLayout);
        
        con.Children.Add(grid);
    }
    public async void btncontinuar_click(object sender, EventArgs e)
    {

        try
        {
            double Total = total * 0.041;
            totalString = Total.ToString("0.00", CultureInfo.InvariantCulture);
            string url = "https://api-m.sandbox.paypal.com/v2/checkout/orders";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "Basic QVVuTmV2OWYxc1pUc2hNNDcyNkhRRzREQ05Jems0S0N1WWhtUGpiOEJPQkh2U0FPd09GdEhyV0wtbG5QZUxyTUZOR3pnWmY3V29xdlVZRTg6RUU2b1ZoQzRzSHlGR2plZU04YjRIS1VFRXVPSUFBYTVRbnFxbjhWOGVZb0xDczFYbF84TjMzWGFoRks3MXJmTmZQZ1c2dERXWkdLRWczNHY=");
            string jsonData = "{\"intent\": \"CAPTURE\", \"purchase_units\": [ { \"reference_id\": \"d9f80740-38f0-11e8-b467-0ed5f89f718b\", \"amount\": { \"currency_code\": \"USD\", \"value\": \"" + totalString + "\" } } ], \"payment_source\": { \"paypal\": { \"experience_context\": { \"payment_method_preference\": \"IMMEDIATE_PAYMENT_REQUIRED\", \"brand_name\": \"Cinepolis\", \"locale\": \"en-US\", \"landing_page\": \"LOGIN\", \"user_action\": \"PAY_NOW\", \"return_url\": \"https://kelwin.epizy.com/payment?totalString=" + totalString+"\" } } } }";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string payerLink;
            string selfLink;

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                JObject jsonResult = JObject.Parse(result);
                string payerActionLink = jsonResult["links"][1]["href"].ToString();
                string selfActionLink = jsonResult["links"][0]["href"].ToString();

                payerLink = payerActionLink;
                selfLink = selfActionLink;
                string Texto = "";
                string txt = await EnviarQR(Texto);
                await Navigation.PushModalAsync(new PaymenState(selfLink, txt, payerLink));

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error de Paypal", ex.Message, "OK");
        }
    }

    public async Task<string> EnviarQR(string Texto) {
        Texto = $"Asientos: {string.Join(",", acientos)}\nSnacks: {string.Join(",", productos.Select(p => $"{p.cantidad} {p.name}"))}";
        try
        {  
           
            if (butom == false)
            {
                butom = true;
                var hisrese = new models.historial_reserva
                {
                    id_usuario = 1,
                    fecha = "2024-03-01",
                    hora = "7:00pm",
                    total = total,
                    id_pelicula = 1
                };
                models.Msg msg = await controllers.historialControllers.CreateHis(hisrese);

                if (msg != null)
                {
                    await LoadData();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
        return Texto;
    }

    public async Task LoadData()
    {
        try
        {
            var posts = await controllers.historialControllers.GetPosts("1");
            if (posts != null)
            {
                // Limpiar la colección actual de Posts
                Posts.Clear();

                idre = posts.ultimo_id_historial;
                await guardar(idre);
                if (productos.Count != 0)
                {
                    await guardarsnck(idre);
                }
                else
                {
                    await DisplayAlert("", "Resevacion exitosa!!", "Ok");
                    await Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                // Manejar caso de posts nulos
                
            }
        }
        catch (Exception ex)
        {
            // Manejar otras excepciones
            await DisplayAlert("Error", $"Error al cargar datos: {ex.Message}", "OK");
        }
    }

    public async Task guardar(int id)
    {
        for(int i=0; i<acientos.Count; i++)
        {
            var hisrese = new models.acientore
            {
                id_pelicula=1,
                aciento= acientos[i],
                ciudad="sps",
                fecha = "2024-03-01",
                hora = "7:00pm",
                id_reserva = id
            };
            models.Msg msg = await controllers.controlleraciento.Createacien(hisrese);

            if (msg != null)
            {

            }
        }

    }

    public async Task guardarsnck(int id)
    {
        bool ms = false;
        for (int i = 0; i <productos.Count; i++)
        {
            var snack = new models.snack
            {
                id_snack = productos[i].id,
                cantiad = productos[i].cantidad,
                precio = productos[i].precio,
                total = productos[i].sub,
                id_reserva = id
            };
            models.Msg msg = await controllers.historial_snackControllers.CreateHis(snack);

            if (msg != null)
            {
                ms= true;
            }
            else
            {
                ms= false;
            }
        }

        if (ms)
        {
            await DisplayAlert("", "Resevacion exitosa!!", "OK");
            await Navigation.PushAsync(new MainPage());
        }
    }
}