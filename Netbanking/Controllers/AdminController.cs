using Core.Application.DTOs.Account;
using Core.Application.Enums;
using Core.Application.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.Products;
using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp.Netbanking.Middleware;

namespace WebApp.Netbanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ILoansService _loansService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserViewModel user;

        public AdminController(IUserService userService,ICreditCardService creditCardService, ILoansService loansService, IHttpContextAccessor httpContext, ISavingsAccountService savingsAccountService)
        {
            _creditCardService = creditCardService;
            _loansService = loansService;
            _httpContext = httpContext;
            _userService = userService;
            _savingsAccountService = savingsAccountService;
            user = _httpContext.HttpContext.Session.Get<UserViewModel>("user");
        }
        
        public IActionResult Index()
        {
            ViewBag.Actual = user.Id;
            return View(_userService.GetAllClients().Result);
        }

        public async Task<IActionResult> DeleteProductCC(string id)
        {
            await _creditCardService.DeleteByStringID(id);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> DeleteProductLoans(string id)
        {
            await _loansService.DeleteByStringID(id);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        public async Task<IActionResult> DeleteProductSavin(string id)
        {
            await _savingsAccountService.DeleteByStringID(id);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        public IActionResult Register(int type)
        {
            ViewBag.Type = type;
            return View(new UserSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel saveViewModel, string infotipo)
        {
            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse registerResponse;

            if (Convert.ToInt32(infotipo) == 1)
            {
                registerResponse = await _userService.RegisterAdmin(saveViewModel, origin);
            }
            else
            {
                registerResponse = await _userService.RegisterClient(saveViewModel, origin);
            }

            if (registerResponse.HasError)
            {
                saveViewModel.HasError = registerResponse.HasError;
                saveViewModel.Error = registerResponse.Error;

                return View(saveViewModel);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> Products(string id)
        {
            ViewBag.UserID = id;
            UserProductsViewModels vm = new();
            vm.TarjetasDeCredito = await _creditCardService.GetAllByUserID(id);
            vm.Prestamos = await _loansService.GetAllByUserID(id);
            vm.CuentasDeAhorro = await _savingsAccountService.GetAllByUserID(id);
            ViewBag.Principal = await _userService.GetSavingByID(id);
            return View(vm);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Admin = await _userService.IsAdmin(id);
            return View(await _userService.GetAccountByid(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];

            if (await _userService.IsAdmin(vm.Id))
            {
                await _userService.UpdateAdmin(vm, origin);
            }
            else
            {
                await _userService.UpdateClient(vm, origin);
            }

            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> CreditCard(double limite, string userid)
        {
            CreditCardSaveViewModel vm = new();
            vm.UserID = userid;
            vm.Limit = limite;
            await _creditCardService.Add(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> Loans(double debt, string userid)
        {
            LoansSaveViewModel vm = new();
            vm.Debt = debt;
            vm.UserID = userid;
            await _loansService.Add(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

        public async Task<IActionResult> SaveAccount(float mont, string userid)
        {
            SavingsAccountSaveViewModel vm = new();
            vm.Amount = mont;
            vm.UserID = userid;
            await _savingsAccountService.Add(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }

    }
}
