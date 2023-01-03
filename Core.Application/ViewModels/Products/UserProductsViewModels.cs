using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Products
{
    public class UserProductsViewModels
    {
        public List<SavingsAccountViewModel> CuentasDeAhorro { get; set; }
        public double TotalCuentas { get; set; }
        public List<CreditCardViewModel> TarjetasDeCredito { get; set; }
        public double DisponibleTarjetas { get; set; }
        public double TarjetasActual { get; set; }
        public double TarjetasAlCorte{ get; set; }
        public List<LoansViewModel> Prestamos { get; set; }
        public double TotalDeudas { get; set; }

    }
}
