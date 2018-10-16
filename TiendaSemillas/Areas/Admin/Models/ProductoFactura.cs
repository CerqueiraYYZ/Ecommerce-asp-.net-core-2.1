using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaSemillas.Areas.Admin.Models
{
    public class ProductoFactura
    {
        public int ProductoID { get; set; }
        public int FacturaID { get; set; }
        public double Cantidad { get; set; }
        public Producto Producto { get; set; }
        public Factura Factura { get; set; }
    }
}
