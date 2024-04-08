using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.models
{
    public class acientore
    {
        public int id {  get; set; }
        public int id_pelicula { get; set; }
        public string aciento { get; set; }
        public string ciudad {  get; set; }
        public string fecha { get; set;}
        public string hora { get; set;}
        public int id_reserva { get; set; }
    }
}
