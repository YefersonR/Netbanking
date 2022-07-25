using Core.Application.ViewModels.Beneficiary;
using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.SavingsAccount
{
    public class SavingsAccountViewModel
    {
        public string AccountNumber { get; set; }
        public float Amount { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }
        public List<TransationsViewModel> Transations { get; set; }
    }
}
