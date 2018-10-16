using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaSemillas.Areas.Admin.Data;
using TiendaSemillas.Areas.Admin.Models;
using TiendaSemillas.Helpers;

namespace TiendaSemillas.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("adminaccount")]
    public class AdminAccountController : Controller
    {
        private readonly ShopContext _context;

        public AdminAccountController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            var administrador = _context.Vendors
                    .Where(b => b.Correo == username)
                    .FirstOrDefault();

            int sessionStatus = 0;
            ViewBag.estado = sessionStatus;
            if (administrador == null)
            {
                return NotFound();
            }

            if (username != null && password != null && username.Equals(administrador.Correo) && password.Equals(administrador.Contrasenna))
            {
                
                HttpContext.Session.SetString("username", administrador.Nombre);
                sessionStatus = 1;
                HttpContext.Session.SetInt32("userID", administrador.ID);
                HttpContext.Session.SetInt32("Islogged", sessionStatus);
                return View("../home/index");
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Create");
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            int sessionStatus = 0;
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("userID");
            HttpContext.Session.SetInt32("Islogged", sessionStatus);
            return View("../Home/Index");
        }
    }
}