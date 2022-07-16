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
        public int Id { get; set; }
        public Guid AccountUser { get; set; }
        public float Amount { get; set; }
        public UserViewModel User { get; set; }
    }
}
