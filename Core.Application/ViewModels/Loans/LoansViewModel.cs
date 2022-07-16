using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.Loans
{
    public class LoansViewModel
    {
        public int Id { get; set; }
        public Guid AccountUser { get; set; }
        public float Amount { get; set; }
    }
}
