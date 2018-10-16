using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaSemillas.Areas.Admin.Data;

namespace TiendaSemillas.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {

        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string correo, string password)
        {

            var client = _context.Clients
                    .Where(b => b.Correo == correo)
                    .FirstOrDefault();

            if (client.Activado)
            {
                int sessionStatus = 0;
                ViewBag.estado = sessionStatus;
                if (client == null)
                {
                    ViewBag.error = "Invalid Account";
                    return View("Create");
                }

                if (correo != null && password != null && correo.Equals(client.Correo) && password.Equals(client.Contrasenna))
                {
                    HttpContext.Session.SetString("username", client.Nombre);
                    sessionStatus = 1;
                    HttpContext.Session.SetInt32("userID", client.ID);
                    HttpContext.Session.SetInt32("logged", sessionStatus);
                    return View("../Home/Index");
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View("Create");
                }
            }
            else
            {
                ViewBag.error = "Cuenta no validada";
                return View("Create");
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            int sessionStatus = 0;
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userID");
            HttpContext.Session.SetInt32("logged", sessionStatus);
            return View("../Home/Index");
        }
        [Route("recuperacion")]
        public IActionResult Recuperacion()
        {
            return View();
        }

        [Route("recuperacionaction")]
        [HttpPost]
        public async Task<IActionResult> RecuperacionAction(string correo)
        {

            var client = _context.Clients
                    .Where(b => b.Correo == correo)
                    .FirstOrDefault();

            if (client == null)
            {
                ViewBag.error = "Invalid Account";
                return View("Create");
            }

            if (correo != null && correo.Equals(client.Correo))
            {
                // servidor SMTP

                SmtpClient clientSmtp = new SmtpClient("smtp.gmail.com");
                clientSmtp.UseDefaultCredentials = false;
                clientSmtp.Credentials = new NetworkCredential("hiberusclaseaspcoremvc@gmail.com", "111??aaa");
                clientSmtp.EnableSsl = true;

                // 
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("alfredocerqueirafoda@gmail.com");
                mailMessage.To.Add(client.Correo);
                mailMessage.Body = "Ingresa a este URL para cambiar su contraseña , https://localhost:44344/Clients/Edit/" + client.ID;
                mailMessage.Subject = "Compras Semillas cambio de contraseña";

                clientSmtp.Send(mailMessage);
                ViewBag.error = "Revise su Correo Electronico";
                View("Create");
                await Task.Delay(3000);
                return View("../Home/Index");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Create");
            }
        }



    }

}