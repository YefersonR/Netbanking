using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Netbanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult beneficiariosAdd()
        {
            return View();
        }

      
            
        public IActionResult Pagos()
        {
            return View();
        }
        public IActionResult Avance_de_efectivo()
        {
            return View();
        }

        public IActionResult Tranferencia()
        {
            return View();
        }
    }
}
