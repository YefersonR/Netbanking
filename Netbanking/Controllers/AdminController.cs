using Core.Application.DTOs.Account;
using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Products;
using Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Register()
        {
            return View(new UserSaveViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserSaveViewModel saveViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(saveViewModel);
            }
            var origin = Request.Headers["origin"];
            RegisterResponse registerResponse = await _userService.RegisterClient(saveViewModel, origin);
            if (registerResponse.HasError)
            {
                saveViewModel.HasError = registerResponse.HasError;
                saveViewModel.Error = registerResponse.Error;

                return View(saveViewModel);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        public async Task<IActionResult> Products()
        {
            UserProductsViewModels vm = new();
            vm.TarjetasDeCredito = await _creditCardService.GetAllAsync();
            vm.Prestamos = await _loansService.GetAllAsync();
            vm.CuentasDeAhorro = await _savingsAccountService.GetAllAsync();
            return View(vm);
        }
    }
}
