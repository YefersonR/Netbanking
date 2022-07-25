using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.SavingsAccount
{
    public class SavingsAccountSaveViewModel
    {
        public string AccountNumber { get; set; }
        public float Amount { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }

    }
}
