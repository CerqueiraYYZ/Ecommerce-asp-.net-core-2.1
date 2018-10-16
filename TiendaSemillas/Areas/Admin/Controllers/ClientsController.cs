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
    public class ClientsController : Controller
    {
        private readonly ShopContext _context;

        public ClientsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Clients
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                return View(await _context.Clients.ToListAsync());
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Clients/Details/5
        //[Route("details")]
        public async Task<IActionResult> Details(int? id)
        {

            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _context.Clients
                    .FirstOrDefaultAsync(m => m.ID == id);

                if (client == null)
                {
                    return NotFound();
                }

                return View(client);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Clients/Create
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

        // POST: Admin/Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Admin/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _context.Clients.FindAsync(id);
                if (client == null)
                {
                    return NotFound();
                }
                return View(client);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Client client)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id != client.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(client);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClientExists(client.ID))
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
                return View(client);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // GET: Admin/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("Islogged") == 1)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _context.Clients
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (client == null)
                {
                    return NotFound();
                }

                return View(client);
            }
            else
            {
                return View("../adminaccount/create");
            }
        }

        // POST: Admin/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
