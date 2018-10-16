using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaSemillas.Areas.Admin.Models;

namespace TiendaSemillas.Areas.Admin.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<ProductoFactura> ProductoFacturas { get; set; }
        public DbSet<Imagenes> Imagenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Vendor>().ToTable("Vendor");
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Factura>().ToTable("Factura");
            modelBuilder.Entity<ProductoFactura>().ToTable("ProductoFactura");
            modelBuilder.Entity<Imagenes>().ToTable("Imagen");

            modelBuilder.Entity<ProductoFactura>()
            .HasKey(c => new { c.ProductoID, c.FacturaID });

        }

    }
}

