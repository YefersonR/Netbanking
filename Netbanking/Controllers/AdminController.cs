using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Netbanking.Controllers
{

    
    public class AdminController : Controller
    {
        private readonly ITransationService _transation;

        public AdminController(ITransationService transation)
        {
            _transation = transation;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products(string ID)
        {

            return View();
        }
    }
}
