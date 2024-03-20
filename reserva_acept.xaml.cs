

using System.Collections.ObjectModel;
using QRCoder;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
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

            try
            {
                string Texto = $"Asientos: {string.Join(",", acientos.Select(a => a.ToString()))}\nSnacks: {string.Join(",", productos.Select(p => $"{p.cantidad} {p.name} - Precio: L.{p.precio} - Total: L.{p.sub}"))}\nTotal: L.{total}";
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(Texto, QRCodeGenerator.ECCLevel.L);
                PngByteQRCode qRCode = new PngByteQRCode(qRCodeData);
                byte[] qrCodeBytes = qRCode.GetGraphic(20);

                MemoryStream qrStream = new MemoryStream(qrCodeBytes);

                string servidor = "smtp.gmail.com";
                int Puerto = 587;
                string GmailUser = "emailcinepolis@gmail.com";
                string GmailPass = "wdxirgcurnjfmxva";

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Pruebas", GmailUser));
                mensaje.To.Add(new MailboxAddress("Destino", "cnombre982@gmail.com"));
                mensaje.Subject = "Aquí está su código QR";

                BodyBuilder CuerpoMensaje = new BodyBuilder();
                CuerpoMensaje.TextBody = "Utiliza este código QR para completar el pago en tu Cinepolis más cercano";

                CuerpoMensaje.Attachments.Add("codigo_qr.png", qrStream);

                mensaje.Body = CuerpoMensaje.ToMessageBody();

                using (SmtpClient ClienteSmtp = new SmtpClient())
                {
                    ClienteSmtp.CheckCertificateRevocation = false;
                    await ClienteSmtp.ConnectAsync(servidor, Puerto, SecureSocketOptions.StartTls);
                    await ClienteSmtp.AuthenticateAsync(GmailUser, GmailPass);
                    await ClienteSmtp.SendAsync(mensaje);
                    await ClienteSmtp.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            if (msg != null)
            {
                await LoadData();
            }
        }

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