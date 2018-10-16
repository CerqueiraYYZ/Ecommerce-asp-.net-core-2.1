using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaSemillas.Areas.Admin.Models
{
    public class Factura
    {
        public int FacturaID { get; set; }
        public double Total { get; set; }
        public DateTime FechaDeCompra { get; set; }
        public Client Client { get; set; }
        public ICollection<ProductoFactura> ProductoFacturas { get; set; }
    }
}
