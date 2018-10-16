using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaSemillas.Areas.Admin.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre {get; set;}
        //el precio es por kilo
        public double Precio { get; set; }
        public double Inventario { get; set; }
        public ICollection<ProductoFactura> ProductoFacturas { get; set; }
        public ICollection<Imagenes> Imagenes { get; set; }
    }
}
