using Core.Application.ViewModels.SavingsAccount;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Beneficiary
{
    public class BeneficiarySaveViewModel
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        [Required(ErrorMessage ="Ingrese una cuenta valida")]
        public string AccountBeneficiary { get; set; }
        public string BeneficiaryID { get; set; }
        public List<UserBeneficiaryViewModel> Beneficiaries{get;set;}
        public bool HasError { get; set; } = false;
        public string Error { get; set; } 
    }
}
