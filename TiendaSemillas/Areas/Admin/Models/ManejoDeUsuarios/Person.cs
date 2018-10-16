using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaSemillas.Areas.Admin.Models
{
    public abstract class Person
    {
        
        public int ID { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasenna { get; set; }
        public DateTime FechaDeIngreso { get; set; }
    }
}
