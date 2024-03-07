

namespace Cinepolis;

public partial class reserva_acept : ContentPage
{
	List<Clase.Producto> productos;
    List<string> acientos;
    int totaacient=0;
    int totsnack = 0;
    int total = 0;
    public reserva_acept(List<Clase.Producto> snack,List<string>acient)
	{
		InitializeComponent();
		this.productos = snack;
        this.acientos = acient;
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
    }
    }