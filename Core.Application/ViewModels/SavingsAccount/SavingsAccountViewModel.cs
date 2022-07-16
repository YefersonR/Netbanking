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
        public Guid AccountNumber { get; set; }
        public float Amount { get; set; }
        public UserViewModel User { get; set; }
        public List<TransationsViewModel> Transations { get; set; }
        public List<BeneficiaryViewModel> Beneficiaries { get; set; }
    }
}
