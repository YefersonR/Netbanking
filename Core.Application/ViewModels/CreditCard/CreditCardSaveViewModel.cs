using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.ViewModels.CreditCard
{
    public class CreditCardSaveViewModel
    {
        public Guid CardNumber { get; set; }
        public float Limit { get; set; }
        public float Debt { get; set; }
        public Guid AccountUser { get; set; }
    }
}
