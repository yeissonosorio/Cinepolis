using System.Collections.ObjectModel;
using System.ComponentModel;
namespace Cinepolis;

public partial class Sala_Reserva : ContentPage
{
    int count = 0;
    private List<string> nombresSeleccionados = new List<string>();
    public ObservableCollection<ElementoModel> Elementos { get; set; }
    public Sala_Reserva()
	{
		InitializeComponent();

        Shell.SetTabBarIsVisible(this, false);
        Elementos = new ObservableCollection<ElementoModel>();
        for (int i = 0; i < 40; i++)
        {
            if (i >= 0 && i <= 7)
            {
                Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = "F" + (41 - (i + 1)) });
            }
            else if (i > 7 && i <= 15)
            {
                Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = "D" + (41 - (i + 1)) });
            }
            else if (i > 15 && i <= 23)
            {
                Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = "C" + (41 - (i + 1)) });
            }
            else if (i > 23 && i <= 31)
            {
                Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = "B" + (41 - (i + 1)) });
            }
            else if (i > 23 && i <= 39)
            {
                Elementos.Add(new ElementoModel { Icono = "iconoa.png", Name = "A" + (41 - (i + 1)) });
            }
        }

        BindingContext = this;
    }

    private async void Image_Tapped(object sender, EventArgs e)
    {
        var image = (Image)sender; // Obtén la imagen que disparó el evento
        var elementoModel = (ElementoModel)image.BindingContext; // Obtén el elemento asociado a la imagen
        var nombre = elementoModel.Name; // Obtén el nombre del elemento
        if (elementoModel.Icono == "iconoa.png")
        {
                elementoModel.SetIcono("iconoac.png");
                count+=60;
                nombresSeleccionados.Add(nombre);
                tot.Text = "L."+count;
            
        }
        else
        {
            elementoModel.SetIcono("iconoa.png");
            count-=60;
            nombresSeleccionados.Remove(nombre);
            tot.Text = "L." + count;
        }
        LabelN.Text = string.Join(", ", nombresSeleccionados);
        if (LabelN.Text == "")
        {
            LabelN.Text = "No hay sillas seleccionadas";
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
}