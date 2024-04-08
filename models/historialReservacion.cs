using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.models
{
    public class historialReservacion
    {
        public int id {  get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public string total { get; set; }
        public string nombre_pelicula { get; set;}
        public string imagen_pelicula { get; set; }
        public int nombre_sala { get; set; }
    }
}
