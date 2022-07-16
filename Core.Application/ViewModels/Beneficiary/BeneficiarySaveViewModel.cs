using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Beneficiary
{
    public class BeneficiarySaveViewModel
    {
        public int Id { get; set; }
        public Guid AccountUser { get; set; }
        public Guid AccountBeneficiary { get; set; }
    }
}
