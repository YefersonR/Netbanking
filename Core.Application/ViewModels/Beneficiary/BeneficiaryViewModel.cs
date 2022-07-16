using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public int Id { get; set; }
        public Guid AccountUser { get; set; }
        public UserViewModel User { get; set; }
        public Guid AccountBeneficiary { get; set; }
        public SavingsAccountViewModel BeneficiaryUser { get; set; }
    }
}
