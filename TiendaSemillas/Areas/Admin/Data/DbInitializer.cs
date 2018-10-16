using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaSemillas.Areas.Admin.Models;

namespace TiendaSemillas.Areas.Admin.Data
{
    public static class DbInitializer
    {
       
            public static void Initialize(ShopContext context)
            {
                context.Database.EnsureCreated();

                // Look for any students.
                if (context.Clients.Any())
                {
                    return;   // DB has been seeded
                }
            Random rnd = new Random();
            var clients = new Client[]
                {
                    new Client{Nombre="Carson",Apellido="Alexander",Correo="Carson@gmail.com",Contrasenna="alksj2",DNI = "87614",FechaDeIngreso=DateTime.Parse("2005-09-01"), Activado = false, Key = Convert.ToString(rnd.Next(1000))},
                    new Client{Nombre="Pepe",Apellido="Jhoan",Correo="asd2@gmail.com",Contrasenna="123h",DNI = "kjh5",FechaDeIngreso=DateTime.Parse("2005-09-01"), Activado = false, Key = Convert.ToString(rnd.Next(1000))},
                    new Client{Nombre="Mark",Apellido="Lopo",Correo="agc3@gmail.com",Contrasenna="987d",DNI = "8kj4",FechaDeIngreso=DateTime.Parse("2005-09-01"), Activado = false, Key = Convert.ToString(rnd.Next(1000))},
                };
                foreach (Client s in clients)
                {
                    context.Clients.Add(s);
                }
                context.SaveChanges();

                var vendors = new Vendor[]
                {
                    new Vendor{Nombre="Carson",Apellido="Alexander",Correo="Carson@gmail.com",Contrasenna="765f",Rol="Administrador Marketing",DNI = "876g",FechaDeIngreso=DateTime.Parse("2005-09-01")},
                    new Vendor{Nombre="Carson",Apellido="Alexander",Correo="Carson@gmail.com",Contrasenna="987a",Rol="Administrador Marketing",DNI = "098hjg",FechaDeIngreso=DateTime.Parse("2005-09-01")},
                    new Vendor{Nombre="Carson",Apellido="Alexander",Correo="Carson@gmail.com",Contrasenna="918n",Rol="Administrador Marketing",DNI = "515kh",FechaDeIngreso=DateTime.Parse("2005-09-01")},
                };
                foreach (Vendor c in vendors)
                {
                    context.Vendors.Add(c);
                }
                context.SaveChanges();

                var productos = new Producto[]
                {
                    new Producto{Nombre="sesamo",Inventario=20.4,Precio=2.0},
                    new Producto{Nombre="lavanda",Inventario=20.4,Precio=2.0},
                    new Producto{Nombre="manzana",Inventario=20.4,Precio=2.0},
                };
                foreach (Producto e in productos)
                {
                    context.Productos.Add(e);
                }
                context.SaveChanges();

        }
    }
}

