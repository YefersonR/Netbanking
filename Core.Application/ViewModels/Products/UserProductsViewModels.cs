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
        public List<CreditCardViewModel> TarjetasDeCredito { get; set; }
        public List<LoansViewModel> Prestamos { get; set; }
    }
}
