using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.models
{
    public class historial_reserva
    {
        public int id {get; set; }
        public int id_usuario { get; set; }
        public string fecha { get; set; }
        public string hora { get; set; }
        public int total { get; set; }
        public int id_pelicula { get; set;}
    }
}
