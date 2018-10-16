using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaSemillas.Areas.Admin.Models;
using TiendaSemillas.Helpers;
using Microsoft.AspNetCore.Http;

namespace TiendaSemillas.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/home")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()

        {
            if (HttpContext.Session.GetInt32("Islogged")==1)
            {
                return View();
            }
            else
            {
                return View("../adminaccount/create");
            }

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public bool ValidacionDeUsuario()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Validacion>>(HttpContext.Session, "isLogged");

            if (cart != null)
            {
                return cart.First().IsLogged;
            }
            else
            {

                return false;
            }
        }
    }
}
