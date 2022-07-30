using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Beneficiary
{
    public class UserBeneficiaryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SavingsAccount { get; set; }
        public DateTime Created { get; set; }
        public bool HasError { get; set; } = false;
        public string Error { get; set; }
    }
}
