using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaSemillas.Areas.Admin.Data;
using TiendaSemillas.Areas.Admin.Models;
using TiendaSemillas.Helpers;
using TiendaSemillas.Models;

namespace TiendaSemillas.Controllers
{
        [Route("cart")]
        public class CartController : Controller
        {
        static double totalAPagar;
        private readonly ShopContext _context;

       

        public CartController(ShopContext context)
        {
            _context = context;
        }

        [Route("index")]
            public IActionResult Index()
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null) {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Products.Precio * item.Quantity);
                totalAPagar = cart.Sum(item => item.Products.Precio * item.Quantity);
                return View();

            }
            else
            {
                return View("../Home/Index");
            }
        }

            [Route("buy/{id}")]
            public async Task<IActionResult> BuyAsync(int id)
            {
            var producto = await _context.Productos
            .FirstOrDefaultAsync(m => m.ProductoID == Convert.ToInt32(id));

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
                {
                    List<Item> cart = new List<Item>();
                    cart.Add(new Item { Products = producto, Quantity = 1 });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                else
                {
                    List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                    int index = isExist(id);
                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new Item { Products =producto, Quantity = 1 });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }
                return RedirectToAction("Index");
            }

            [Route("remove/{id}")]
            public IActionResult Remove(int id)
            {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            //Nuevo arreglo
            if (cart[index].Quantity > 1)
            {

                cart[index].Quantity = cart[index].Quantity - 1;
            }
            else
            {
                cart.RemoveAt(index);
            }
            //end
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
            }

            private int isExist(int id)
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].Products.ProductoID.Equals(Convert.ToInt32(id)))
                    {
                        return i;
                    }
                }
                return -1;
            }

        [Route("finalizacompra/{id}")]
        public async Task<IActionResult> FinalizaCompraAsync(int id)
        {
            
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            List<Producto> productos = new List<Producto>();


            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == id);

            if (cart.Count == 0) {
                //Inicializar insertando un dato a la tabla Factura con su respectivo ClientID
                Factura facturaOb = new Factura { Client = client, FechaDeCompra = DateTime.Now, Total = totalAPagar };
                _context.Add(facturaOb);
                await _context.SaveChangesAsync();

                //Inserto en la tabla N:M ProductoFactura
                ProductoFactura productoFactura;
                foreach (var item in cart)
                {
                    productoFactura = new ProductoFactura { ProductoID = item.Products.ProductoID, Cantidad = item.Quantity, FacturaID = facturaOb.FacturaID };
                    _context.Add(productoFactura);
                    await _context.SaveChangesAsync();
                }

                //Reinicio el carrito de compras
                cart = new List<Item>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                //_context.Add(factura);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Gracias/" + facturaOb.FacturaID);
            }
            else
            {
                return View("../Home/Index");
            }

            //SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            //return RedirectToAction("Index");
        }
        [Route("gracias/{id}")]
        public async Task<IActionResult> Gracias(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(s => s.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.FacturaID == id);
            
           var productoFacturas = await _context.ProductoFacturas.ToListAsync();
            List<ProductoFactura> productos = new List<ProductoFactura>();

            foreach (var item in productoFacturas)
            {
                if (item.FacturaID == factura.FacturaID)
                {
                    productos.Add(item);
                }
            }

            foreach (var item in productos)
            {

                item.Producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(m => m.ProductoID == item.ProductoID);

            }

            factura.ProductoFacturas = productos;

            ViewBag.listaProductos = productos;

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }


    }
}