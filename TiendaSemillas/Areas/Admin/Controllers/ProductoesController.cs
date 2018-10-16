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
using System.IO;

namespace TiendaSemillas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoesController : Controller
    {
        private readonly ShopContext _context;

        public ProductoesController(ShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Productoes
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                return View(await _context.Productos.Include(s => s.Imagenes).ToListAsync());
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos
                .Include(s => s.Imagenes)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ProductoID == id);

                if (producto == null)
                {
                    return NotFound();
                }

                return View(producto);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Productoes/Create
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

        // POST: Admin/Productoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoID,Nombre,Precio,Inventario")] Producto producto, IFormFile[] photos)
        {
            if (ModelState.IsValid)
            {
                producto.Imagenes = new List<Imagenes>();
                foreach (IFormFile photo in photos)
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + producto.Nombre));
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + producto.Nombre, photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    producto.Imagenes.Add(new Imagenes { nombre = photo.FileName });
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Admin/Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }
                return View(producto);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoID,Nombre,Precio,Inventario")] Producto producto, IFormFile[] photos)
        {
            if (id != producto.ProductoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                producto.Imagenes = new List<Imagenes>();
                foreach (IFormFile photo in photos)
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + producto.Nombre));
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/" + producto.Nombre, photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    producto.Imagenes.Add(new Imagenes { nombre = photo.FileName });
                }

                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoID))
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
            return View(producto);
        }

        // GET: Admin/Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos
                    .FirstOrDefaultAsync(m => m.ProductoID == id);
                if (producto == null)
                {
                    return NotFound();
                }

                return View(producto);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoID == id);
        }
    }
}
