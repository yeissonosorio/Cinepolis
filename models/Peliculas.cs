using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.models
{
    public class Peliculas
    {
        public int id {  get; set; }    
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int sala {  get; set; }
        public string imagen {  get; set; }
        public int estado { get; set; }
    }
}
