using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.Transation;
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
        private readonly ITransationService _transationService;
        private readonly ICreditCardService _CreditCardService;


        public ClientController(ITransationService transationService)
        {
            _transationService = transationService;
        }
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
            return View(new TransationsSaveViewModel());
        }
        [HttpPost]
        public IActionResult Pagos(TransationsSaveViewModel loans)
        {
            _transationService.PayLoans(loans);
            return View();
        }
        public IActionResult Avance_de_efectivo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Avance_de_efectivo(TransationsSaveViewModel transationsSave)
        {
            _transationService.RetireToCard(transationsSave);
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        public IActionResult Tranferencia()
        {
            return View(new TransationsSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Tranferencia(TransationsSaveViewModel transations )
        {
            await _transationService.PayToAccount(transations);
            return RedirectToRoute(new { controller = "Home",action="Index" });
        }

        public IActionResult Prestamos()
        {
            return View();
        }
        public IActionResult Beneficiario()
        {
            return View();
        }
        public IActionResult Tarjeta_de_credito()
        {
            return View();
        }
        public IActionResult Expreso()
        {
            return View();
        }
    }
}
