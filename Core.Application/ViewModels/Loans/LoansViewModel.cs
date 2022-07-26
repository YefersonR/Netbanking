using Core.Application.ViewModels.Transation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Loans
{
    public class LoansViewModel
    {
        public string Loan { get; set; }
        public double Debt { get; set; }
        public double Limit { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }
        public List<TransationsViewModel> Transations { get; set; }

    }
}
