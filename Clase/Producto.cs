using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinepolis.Clase
{
    public class Producto
    {
        public int id { get; set; }
        public string? name { get; set; }

        public int cantidad { get; set; }
        public int precio { get; set; }
        public int sub { get; set; }
    }
}
