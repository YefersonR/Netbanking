using Core.Application.ViewModels.CreditCard;
using Core.Application.ViewModels.Loans;
using Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Transation
{
    public class TransationsInfoViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        public string UserToPayAccount { get; set; }
        public DateTime Created { get; set; }
        public string SavingsAccount { get; set; }
        public string CreditCard { get; set; }
        public string Loans { get; set; }

    }
}
