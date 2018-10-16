using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaSemillas.Areas.Admin.Models
{
    public class Client : Person
    {
        public bool Activado { get; set; }
        public string Key { get; set; }
        public ICollection<Factura> Facturas { get; set; }
    }
}
