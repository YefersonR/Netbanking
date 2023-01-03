using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.Products;
using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
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
            userProducts.TotalCuentas = await _SavingAccountService.TotalActual();
            userProducts.TarjetasDeCredito = await _CreditCardService.GetAllAsync();
            userProducts.DisponibleTarjetas = await _CreditCardService.DisponibleTarjeta();
            userProducts.TarjetasActual = await _CreditCardService.TotalTarjeta();
            userProducts.TarjetasAlCorte = await _CreditCardService.TarjetaAlCorte();
            userProducts.Prestamos = await _loansService.GetAllAsync();
            userProducts.TotalDeudas = await _loansService.TotalDeudas();
            return View(userProducts);
        }

        public async Task<IActionResult> beneficiariosAdd()
        {
            BeneficiarySaveViewModel Beneficiaries = new();
            Beneficiaries.Beneficiaries = await _BeneficiaryService.GetUserBeneficiary();
            return View(Beneficiaries);
        }

        [HttpPost]
        public async Task<IActionResult> beneficiariosAdd(BeneficiarySaveViewModel beneficiary)
        {
            if (!ModelState.IsValid)
            {
                beneficiary.Beneficiaries = await _BeneficiaryService.GetUserBeneficiary();
            }
            var beneficiaryResult = await _BeneficiaryService.AddBeneficiary(beneficiary);
            if (beneficiaryResult.HasError)
            {
                ViewBag.Error = beneficiaryResult.Error;
                beneficiary.Error = beneficiaryResult.Error;
                beneficiary.HasError = beneficiaryResult.HasError;
            }
            return RedirectToRoute(new { controller ="Client", action="beneficiariosAdd"});

        }
        public async Task<IActionResult> beneficiarioDelete(int ID)
        {
            await _BeneficiaryService.DeleteBeneficiary(ID);
            return View("beneficiariosAdd");
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
            if (transationsSave.CardNumber is null || transationsSave.Amount == 0 || transationsSave.AccountNumber is null)
            {
                transationsSave.savingsAccounts = await _SavingAccountService.GetAllAsync();
                transationsSave.CreditCards = await _CreditCardService.GetAllAsync();
                return View(transationsSave);
            }
            await _transationService.RetireToCard(transationsSave);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Tranferencia()
        {
            TransationsSaveViewModel transations = new();
            transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
            return View(transations);
        }
        [HttpPost]
        public async Task<IActionResult> Tranferencia(TransationsSaveViewModel transations)
        {
            if (transations.AccountNumber is null || transations.Amount == 0 || transations.UserToPayAccount is null)
            {
                transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
                return View(transations);
            }
            await _transationService.PayToAccount(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Prestamos()
        {
            TransationsSaveViewModel transationsCard = new();
            transationsCard.savingsAccounts = await _SavingAccountService.GetAllAsync();
            transationsCard.Loans = await _loansService.GetAllAsync();
            return View(transationsCard);
        }
        [HttpPost]
        public async Task<IActionResult> Prestamos(TransationsSaveViewModel transations)
        {
            if (transations.AccountNumber is null || transations.Amount == 0 || transations.Loan is null)
            {
                transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
                transations.Loans = await _loansService.GetAllAsync();
                return View(transations);
            }
            await _transationService.PayLoans(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
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
            if (transations.AccountNumber is null || transations.Amount == 0 || transations.CardNumber is null)
            {
                transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
                transations.CreditCards = await _CreditCardService.GetAllAsync();
                return View(transations);
            }
            await _transationService.PayToCard(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }

        public async Task<IActionResult> Expreso(TransationsSaveViewModel transation = null)
        {
            TransationsSaveViewModel transations = new();
            
            transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
            return View(transations);
        }
        [HttpPost]
        public async Task<IActionResult> PagoExpreso(TransationsSaveViewModel transations)
        {
            if (transations.AccountNumber is null || transations.Amount == 0 || transations.UserToPayAccount is null)
            {
                var Beneficiary = await _BeneficiaryService.GetUserBeneficiary();
                transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
                ViewBag.beneficiary = Beneficiary;
                return View("Beneficiario",transations);
            }
            await _transationService.PayToAccount(transations);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
        }
        public async Task<IActionResult> Beneficiario(string savingaccount = null)
        {
            TransationsSaveViewModel transations = new();
            transations.savingsAccounts = await _SavingAccountService.GetAllAsync();
            var Beneficiary = await _BeneficiaryService.GetUserBeneficiary();
            ViewBag.beneficiary = Beneficiary;
            if(savingaccount != null)
            {
                transations.UserToPayAccount = savingaccount;
            }

            return View(transations);
        }
        [HttpPost]
        public async Task<IActionResult> Confirmacion(TransationsSaveViewModel transation)
        {
            if (transation.AccountNumber is null || transation.Amount == 0 || transation.UserToPayAccount is null)
            {
                transation.savingsAccounts = await _SavingAccountService.GetAllAsync();
                return View("Expreso", transation);
            }

            UserConfirmationViewModel userInfo = await _BeneficiaryService.GetUserByAccount(transation.UserToPayAccount);
            ViewBag.userInfo = userInfo;
            return View(transation);
        }
    }
}
