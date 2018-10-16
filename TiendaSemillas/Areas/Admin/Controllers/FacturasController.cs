using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaSemillas.Areas.Admin.Data;
using TiendaSemillas.Areas.Admin.Models;
using Microsoft.AspNetCore.Http;

namespace TiendaSemillas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacturasController : Controller
    {
        private readonly ShopContext _context;

        public FacturasController(ShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Facturas
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                return View(await _context.Facturas.ToListAsync());
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (HttpContext.Session.GetInt32("Islogged") == 1)
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
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Facturas/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                return View();
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaID,Total,FechaDeCompra")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Admin/Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var factura = await _context.Facturas.FindAsync(id);
                if (factura == null)
                {
                    return NotFound();
                }
                return View(factura);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaID,Total,FechaDeCompra")] Factura factura)
        {
            if (id != factura.FacturaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.FacturaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Admin/Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var factura = await _context.Facturas
                    .FirstOrDefaultAsync(m => m.FacturaID == id);
                if (factura == null)
                {
                    return NotFound();
                }

                return View(factura);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.FacturaID == id);
        }
    }
}
