using Core.Application.DTOs.Account;
using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Products;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
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

        public AdminController(IUserService userService,ICreditCardService creditCardService, ILoansService loansService, ISavingsAccountService savingsAccountService)
        {
            _creditCardService = creditCardService;
            _loansService = loansService;
            _userService = userService;
            _savingsAccountService = savingsAccountService;
        }
        
        public IActionResult Index()
        {
            ViewBag.Actual = "90b5fc06-f8aa-4e24-8371-6920d33823a6";//Pasar user actual
            return View(_userService.GetAllClients().Result);
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
            UserProductsViewModels vm = new();
            vm.TarjetasDeCredito = await _creditCardService.GetAllByUserID(id);
            vm.Prestamos = await _loansService.GetAllByUserID(id);
            vm.CuentasDeAhorro = await _savingsAccountService.GetAllByUserID(id);
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
    }
}
