using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaSemillas.Areas.Admin.Models;

namespace TiendaSemillas.Models
{
    public class Item
    {
        public Producto Products { get; set; }
        public double Quantity { get; set; }
    }
}
