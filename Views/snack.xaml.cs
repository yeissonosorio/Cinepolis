using System.ComponentModel;
using System.Windows.Input;

namespace Cinepolis;

public partial class snack : ContentPage
{

    int[] count = { 0, 0, 0 };
    int total = 0;
    int[] sub = { 0, 0, 0 };

    List<string> nomacientos;
    int Id_pelicula;
    string Ciudad;
    string Fecha;
    string Hora;
    string imagen;
    string Nombre;
    string FechaI;
    public snack(List<string> acientos, int pel, string ciu, string fech, string hor, string ima,string nombre,string fechai)
    {

        InitializeComponent();
        this.nomacientos = acientos;
        Shell.SetTabBarIsVisible(this, false);
        Id_pelicula = pel;
        Ciudad = ciu;
        Fecha = fech;
        Hora = hor;
        imagen = ima;
        Nombre = nombre;
        FechaI = fechai;
    }

    public async void mas_Cliked(object sender, EventArgs e)
    {
        count[0]++;
        snack1.Text = count[0].ToString();
        total += 60;
        tot.Text = total.ToString();
    }

    public async void menos_Cliked(object sender, EventArgs e)
    {
        if (count[0] != 0)
        {
            count[0]--;
            snack1.Text = count[0].ToString();
            total -= 60;
            tot.Text = total.ToString();
        }
    }

    public async void mas1_Cliked(object sender, EventArgs e)
    {
        count[1]++;
        snack2.Text = count[1].ToString();
        total += 140;
        tot.Text = total.ToString();
    }

    public async void menos1_Cliked(object sender, EventArgs e)
    {
        if (count[1] != 0)
        {
            count[1]--;
            snack2.Text = count[1].ToString();
            total -= 140;
            tot.Text = total.ToString();
        }
    }

    public async void mas2_Cliked(object sender, EventArgs e)
    {
        count[2]++;
        snack3.Text = count[2].ToString();
        total += 180;
        tot.Text = total.ToString();
    }

    public async void menos2_Cliked(object sender, EventArgs e)
    {
        if (count[2] != 0)
        {
            count[2]--;
            snack3.Text = count[2].ToString();
            total -= 180;
            tot.Text = total.ToString();
        }
    }

    public async void btncontinuar_click(object sender, EventArgs e)
    {
        List<Clase.Producto> listCombo = new List<Clase.Producto>();
        if (snack1.Text != "0" || snack2.Text != "0" || snack3.Text != "0")
        {
            if (snack1.Text != "0")
            {
                sub[0] = count[0] * 60;
                Clase.Producto pro = new Clase.Producto { id = 1, name = "Nachos", cantidad = count[0], precio = 60, sub = sub[0] };
                listCombo.Add(pro);
            }
            if (snack2.Text != "0")
            {
                sub[1] = count[1] * 140;
                Clase.Producto pro = new Clase.Producto { id = 2, name = "Combo 1", cantidad = count[1], precio = 140, sub = sub[1] };
                listCombo.Add(pro);
            }
            if (snack3.Text != "0")
            {
                sub[2] = count[2] * 180;
                Clase.Producto pro = new Clase.Producto { id = 3, name = "Combo 2", cantidad = count[2], precio = 180, sub = sub[2] };
                listCombo.Add(pro);
            }
            await Navigation.PushAsync(new reserva_acept(listCombo, nomacientos,Id_pelicula,Ciudad,Fecha,Hora,imagen,Nombre,FechaI));
        }
        else
        {
            await DisplayAlert("Aviso", "Seleccione un snack", "Ok");
        }
    }
}