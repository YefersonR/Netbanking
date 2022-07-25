using Core.Application.ViewModels.Transation;
using Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Loans
{
    public class LoansSaveViewModel
    {
        public string Loan { get; set; }
        public double Debt { get; set; }
        public double Limit { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }
    }
}

