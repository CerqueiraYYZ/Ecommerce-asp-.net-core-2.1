using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaSemillas.Areas.Admin.Data;
using TiendaSemillas.Areas.Admin.Models;

namespace TiendaSemillas.Controllers
{

    public class ClientsController : Controller
    {
        private readonly ShopContext _context;

        public ClientsController(ShopContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> FinalizaCreacion(string id, string key)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ID == Convert.ToInt32(id));

            if (client.Key == key)
            {
                client.Activado = true;
                _context.Update(client);
                await _context.SaveChangesAsync();
            }


            return View("../Home/Index");
        }


        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Activado,Key,ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Client client)
        {
            Random random = new Random();
            if (ModelState.IsValid)
            {
                client.Activado = false;
                client.Key = Convert.ToString(random.Next(1000));

                _context.Add(client);
                await _context.SaveChangesAsync();

                // servidor SMTP

                SmtpClient clientSmtp = new SmtpClient("smtp.gmail.com");
                clientSmtp.UseDefaultCredentials = false;
                clientSmtp.Credentials = new NetworkCredential("hiberusclaseaspcoremvc@gmail.com", "111??aaa");
                clientSmtp.EnableSsl = true;

                // 
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("alfredocerqueirafoda@gmail.com");
                mailMessage.To.Add(client.Correo);
                mailMessage.Body = "Ingresa a este URL para finalizar la creacion de tu Usuario, https://localhost:44344/clients/finalizacreacion?id=" + client.ID + "&key=" + client.Key;
                mailMessage.Subject = "Compras Semillas";

                clientSmtp.Send(mailMessage);

                return View("../Account/Create");
            }
            return View("../Account/Create");
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Key,ID,DNI,Nombre,Apellido,Correo,Contrasenna,FechaDeIngreso")] Client client)
        {
            if (id != client.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.Activado = true;

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
                return RedirectToAction("../Clients/Details/"+client.ID);
            }
            return View("../Home/Index");
        }


      

       

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ID == id);
        }
    }
}
