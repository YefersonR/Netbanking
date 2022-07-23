using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Netbanking.Controllers
{

    
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

        public async Task<IActionResult> Products(string ID)
        {
            UserProductsViewModels vm = new();
            vm.TarjetasDeCredito = await _creditCardService.GetAllByUserID(ID);
            vm.Prestamos = await _loansService.GetAllByUserID(ID);
            vm.CuentasDeAhorro = await _savingsAccountService.GetAllByUserID(ID);
            return View(vm);
        }
    }
}
