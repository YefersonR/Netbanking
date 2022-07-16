using Core.Application.ViewModels.SavingsAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Transation
{
    public class TransationsViewModel
    {
        public int Id { get; set; }
        public Guid AccountNumber { get; set; }
        public float Amount { get; set; }
        public Guid NumberAccountToPay { get; set; }
        public SavingsAccountViewModel User { get; set; }
        public SavingsAccountViewModel AccountToPay { get; set; }
    }
}
