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
    public class VendorsController : Controller
    {
        private readonly ShopContext _context;

        public VendorsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Vendors
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                return View(await _context.Vendors.ToListAsync());
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Vendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vendor = await _context.Vendors
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return View(vendor);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Vendors/Create
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

        // POST: Admin/Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rol,ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendor);
        }

        // GET: Admin/Vendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }
                return View(vendor);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rol,ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Vendor vendor)
        {
            if (id != vendor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.ID))
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
            return View(vendor);
        }

        // GET: Admin/Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vendor = await _context.Vendors
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (vendor == null)
                {
                    return NotFound();
                }

                return View(vendor);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.ID == id);
        }
    }
}
