using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Transation
{
    public class TransationsSaveViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public string UserToPayAccount { get; set; }
        public string CardNumber { get; set; }
        public List<SavingsAccountViewModel> savingsAccounts { get; set; }
        public List<SavingsAccountViewModel> UserToPayAccounts { get; set; }

        public List<CreditCardViewModel> CreditCards { get; set; }


    }
}
