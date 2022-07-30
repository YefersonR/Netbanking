using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.Products;
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
        private readonly IBeneficiaryService _BeneficiaryService;
        private readonly ISavingsAccountService _SavingAccountService;
        private readonly ILoansService _loansService;

        public ClientController(ILoansService loansService, ICreditCardService CreditCardService, IBeneficiaryService BeneficiaryService, ITransationService transationService, ISavingsAccountService SavingAccountService)
        {
            _transationService = transationService;
            _BeneficiaryService = BeneficiaryService;
            _SavingAccountService = SavingAccountService;
            _CreditCardService = CreditCardService;
            _loansService = loansService;

        }
        public async Task<IActionResult> Index()
        {
            UserProductsViewModels userProducts = new();
            userProducts.CuentasDeAhorro = await _SavingAccountService.GetAllAsync();
            userProducts.TarjetasDeCredito = await _CreditCardService.GetAllAsync();
            userProducts.Prestamos = await _loansService.GetAllAsync();
            return View(userProducts);
        }

        public async Task<IActionResult> beneficiariosAdd()
        {
            BeneficiarySaveViewModel Beneficiaries = new();
            Beneficiaries.Beneficiaries = await _BeneficiaryService.GetUserBeneficiary();
            return View(Beneficiaries);
        }

        [HttpPost]
        public async Task<IActionResult> beneficiarioAdd(BeneficiarySaveViewModel beneficiary)
        {
            await _BeneficiaryService.Add(beneficiary);
            return View(beneficiary);
        }

        public IActionResult Pagos()
        {
            return View(new TransationsSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Pagos(TransationsSaveViewModel loans)
        {
            await _transationService.PayLoans(loans);
            return View();
        }
        public async Task<IActionResult> Avance_de_efectivo()
        {
            TransationsSaveViewModel transationsCard = new();
            transationsCard.savingsAccounts = await _SavingAccountService.GetAllAsync();
            transationsCard.CreditCards = await _CreditCardService.GetAllAsync();
            return View(transationsCard);
        }
        [HttpPost]
        public async Task<IActionResult> Avance_de_efectivo(TransationsSaveViewModel transationsSave)
        {
            await _transationService.RetireToCard(transationsSave);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Tranferencia()
        {
            TransationsSaveViewModel transations = new();
            transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
            transations.UserToPayAccounts = await _SavingAccountService.GetAllAsync();
            return View(transations);
        }
        [HttpPost]
        public async Task<IActionResult> Tranferencia(TransationsSaveViewModel transations)
        {
            await _transationService.PayToAccount(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public IActionResult Prestamos()
        {
            return View();
        }

        public async Task<IActionResult> Tarjeta_de_credito()
        {
            TransationsSaveViewModel transationsCard = new();
            transationsCard.savingsAccounts = await _SavingAccountService.GetAllAsync();
            transationsCard.CreditCards = await _CreditCardService.GetAllAsync();
            return View(transationsCard);
        }
        [HttpPost]
        public async Task<IActionResult> Tarjeta_de_credito(TransationsSaveViewModel transations)
        {
            await _transationService.PayToCard(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Expreso(string savingaccount)
        {
            TransationsSaveViewModel transations = new();
            transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
            transations.UserToPayAccount = savingaccount;

            return View(transations);
        }
        [HttpPost]
        public async Task<IActionResult> Expreso(TransationsSaveViewModel transations)
        {
            await _transationService.PayToAccount(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
    }
}
