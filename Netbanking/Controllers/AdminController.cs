using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Netbanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly ILoansService _loansService;
        private readonly ISavingsAccountService _savingsAccountService;

        public AdminController(ICreditCardService creditCardService, ILoansService loansService, ISavingsAccountService savingsAccountService)
        {
            _creditCardService = creditCardService;
            _loansService = loansService;
            _savingsAccountService = savingsAccountService;
        }
        
        public IActionResult Index()
        {
            return View();
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
